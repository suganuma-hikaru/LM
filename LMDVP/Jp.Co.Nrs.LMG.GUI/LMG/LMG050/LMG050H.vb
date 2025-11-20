' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG050H : 請求処理 請求書作成
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMG050ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG050H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMG050F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMG050V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMG050G

    ''' <summary>
    ''' 共通画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMGControlG

    ''' <summary>
    ''' 共通Validateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMGControlV

    ''' <summary>
    ''' 共通Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlH As LMGControlH

    ''' <summary>
    ''' パラメータ格納用DataSet
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' 検索結果格納用DataSet(明細更新用にも使用)
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet = New LMG050DS

    ''' <summary>
    ''' 計算処理を行ったかどうかを把握するためのフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _KeisanFlg As Boolean = False

    ''' <summary>
    ''' 最大枝番
    ''' </summary>
    ''' <remarks></remarks>
    Private _EdaNo As Integer = 0

    ''' <summary>
    ''' 経理戻し済みフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _KeiriModoshi_Flg As Boolean = False

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private _SeikyuCd As String = String.Empty

    ''' <summary>
    ''' コンボボックス用データセット
    ''' </summary>
    Private _dsCombo As DataSet = Nothing

    ''' <summary>
    ''' 請求先が TSMC か否か
    ''' </summary>
    Private _IsTsmc As Boolean = False

    ''' <summary>
    ''' TSMC か否かを最後に判定した請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private _SeikyuCdIsTsmc As String = String.Empty

    ''' <summary>
    ''' 取込データ件数(TSMC)
    ''' </summary>
    Private _TsmcImpCnt As Integer = 0

#End Region

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm">パラメータ</param>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれる</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        '受け取ったパラメータを設定
        Me._PrmDs = prm.ParamDataSet
        Dim prmDr As DataRow = Me._PrmDs.Tables(LMG050C.TABLE_NM_IN).Rows(0)

        Me._PrmDs.Tables(LMG050C.TABLE_NM_IN).Rows(0).Item("KBN_LANG") = If("0".Equals(lgm.MessageLanguage()), "ja", "en")

        'フォームの作成
        Me._Frm = New LMG050F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMGControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMGControlV(Me, DirectCast(Me._Frm, Form))

        'Handler共通クラスの設定
        Me._ControlH = New LMGControlH(DirectCast(Me._Frm, Form), "LMG050", Me._ControlV, Me._ControlG)

        'Gamenクラスの設定
        Me._G = New LMG050G(Me, Me._Frm, Me._ControlG, Me._ControlH)

        'Validateクラスの設定
        Me._V = New LMG050V(Me, Me._Frm, Me._ControlV)

        'フォームの初期化
        MyBase.InitControl(Me._Frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(Me._Frm)
        '2015.10.15 英語化対応END

        'モード・ステータスの設定
        Dim dispMd As String = String.Empty
        Dim recStat As String = String.Empty
        If String.IsNullOrEmpty(prmDr.Item("CRT_KB").ToString()) Then
            dispMd = DispMode.VIEW
            recStat = RecordStatus.NOMAL_REC
        Else
            dispMd = DispMode.EDIT
            recStat = RecordStatus.NEW_REC
        End If
#If True Then   'ADD 2018/08/10 依頼番号 : 002136  
        If ("99").Equals(prmDr.Item("CRT_KB").ToString()) Then
            dispMd = DispMode.VIEW
            recStat = RecordStatus.DELETE_REC
        End If

#End If
        Call Me._G.SetModeAndStatus(dispMd, recStat)

        'Enter押下イベントの設定
        Call Me._ControlG.SetEnterEvent(Me._Frm.sprSeikyuM)

        'イベント設定
        Call Me.SetEventHandler()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コンボボックス用の値取得
        Me.GetComboData()

        'コントロール個別設定
        Call Me._G.SetControl(prmDr)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        '要望番号:1935 yamanaka 2013.03.08 Start
        '検索処理を行う
        If dispMd.Equals(DispMode.VIEW) Then
            '検索処理を行う
            Me.SelectData()
        Else
            '初期値設定
            Call Me._G.SetDataControl(prmDr, MyBase.GetSystemDateTime(0))
        End If

        '2018/04/06 001225【LMS】請求鑑_全自動の場合は経理控を印刷しない(横浜石井) Annen add start
        Call Me._G.SetKeiriHikae()
        '2018/04/06 001225【LMS】請求鑑_全自動の場合は経理控を印刷しない(横浜石井) Annen add end

        '通貨コード
        If Not Me.setCurrList() Then
            '終了（画面も出さない）
            Me._Frm.Close()
            Exit Sub
        End If

        ''2014.08.21 追加START 多通貨対応
        ''コンボコントロールの設定
        'Call Me._G.SetComboControl(ds)
        ' ''2014.08.21 追加END 多通貨対応

        'If dispMd.Equals(DispMode.VIEW) Then
        '    '検索処理を行う
        '    Me.SelectData()
        'End If
        '要望番号:1935 yamanaka 2013.03.08 End

        'メッセージエリアの設定
        Call Me.SetBaseMsg()

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._KeiriModoshi_Flg)

        '外部倉庫用ABP対策
        Dim drABP As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'G203' AND KBN_NM1 = '", LM.Base.LMUserInfoManager.GetNrsBrCd, "'"))
        If drABP.Length > 0 Then
            'SAP伝票番号を非表示
            Me._Frm.lblTitleSapNo.Visible = False
            Me._Frm.lblSapNo.Visible = False
            'スプレッドの真荷主～地域セグメント(着地)を非表示
            Me._Frm.sprSeikyuM.ActiveSheet.Columns(LMG050C.SprColumnIndex.TCUST_BPCD).Visible = False
            Me._Frm.sprSeikyuM.ActiveSheet.Columns(LMG050C.SprColumnIndex.TCUST_BPNM).Visible = False
            Me._Frm.sprSeikyuM.ActiveSheet.Columns(LMG050C.SprColumnIndex.PRODUCT_SEG_CD).Visible = False
            Me._Frm.sprSeikyuM.ActiveSheet.Columns(LMG050C.SprColumnIndex.ORIG_SEG_CD).Visible = False
            Me._Frm.sprSeikyuM.ActiveSheet.Columns(LMG050C.SprColumnIndex.DEST_SEG_CD).Visible = False
        End If

        ' 請求先判定(TSMCか否か)
        Me._IsTsmc = Me.IsTsmc()
        ' TSMCか否かによる 取込項目の表示制御と初期値制御
        Me._G.SetControlsStatus2(Me._IsTsmc, isInit:=True)

        'フォーカスの設定
        Call Me.SetFocus()

        'フォームの表示
        Me._Frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditEvent()

        Dim msg As String = "編集処理"

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMG050C.EventShubetsu.EDIT) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsCommonChk(msg) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        'ADD START 2023/05/02 035323【LMS】経理取込み対象外後の鑑編集
        '進捗区分が「経理取込対象外」の場合、処理続行確認
        If Me._Frm.cmbStateKbn.SelectedValue.Equals(LMG050C.STATE_KEIRI_TAISHO_GAI) Then
            If MyBase.ShowMessage(Me._Frm, "W316") = MsgBoxResult.Cancel Then
                Call Me.SetBaseMsg()    'メッセージの設定
                Call Me.EndAction()     '終了処理
                Exit Sub
            End If
        End If
        'ADD END   2023/05/02 035323【LMS】経理取込み対象外後の鑑編集

        'DataSet設定
        Dim ds As DataSet = New LMG050DS()
        Call Me.SetDataSetEventData(ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "HaitaChk")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG050BLF", "HaitaChk", ds)

        'メッセージコードの判定
        If MyBase.IsMessageExist = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "HaitaChk")

        'ADD START 2023/05/02 035323【LMS】経理取込み対象外後の鑑編集
        If Me._Frm.cmbStateKbn.SelectedValue.Equals(LMG050C.STATE_KEIRI_TAISHO_GAI) Then
            '進捗区分が「経理取込対象外」の場合、「未確定」に変更
            Me._Frm.cmbStateKbn.SelectedValue = LMG050C.STATE_MIKAKUTEI
        End If
        'ADD END   2023/05/02 035323【LMS】経理取込み対象外後の鑑編集

        '終了処理
        Call Me.EndAction()

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(Com.Const.DispMode.EDIT, Com.Const.RecordStatus.NOMAL_REC)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._KeiriModoshi_Flg)

        'SPREADの再描画
        Call Me._G.SetSpread(Me._FindDs.Tables(LMG050C.TABLE_NM_DTL), _dsCombo)

        '計算処理を行う
        Call Me.SetCalcAll()

        'メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G003")

        'フォーカスの設定
        Call Me.SetFocus()

    End Sub

#If True Then   ''ADD 2018/08/21 依頼番号 : 002136 
    ''' <summary>
    ''' 復活処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FukkatsuEvent()

        Dim msg As String = "復活処理"

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック　--削除と同じ
        If Me._V.IsAuthorityChk(LMG050C.EventShubetsu.DELETE) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsCommonChk(msg) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(msg) = False Then
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMG050DS()
        Call Me.SetDataSetEventData(ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "FukkatsuData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG050BLF", "FukkatsuData", ds)

        'メッセージコードの判定
        If MyBase.IsMessageExist = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "FukkatsuData")

        '終了処理  
        Me._Frm.Close()


    End Sub
#End If

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteEvent()

        Dim msg As String = "削除処理"

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMG050C.EventShubetsu.DELETE) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsCommonChk(msg) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(msg) = False Then
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMG050DS()
        Call Me.SetDataSetEventData(ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG050BLF", "DeleteData", ds)

        'メッセージコードの判定
        If MyBase.IsMessageExist = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        '終了処理  
        Me._Frm.Close()


    End Sub

    ''' <summary>
    ''' 確定処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub KakuteiEvent()

        Call Me.StageUp(LMG050C.EventShubetsu.KAKUTEI)

        '2018/04/09 001225【LMS】請求鑑_全自動の場合は経理控を印刷しない(横浜石井) Annen add start
        Call Me._G.SetKeiriHikae()
        '2018/04/09 001225【LMS】請求鑑_全自動の場合は経理控を印刷しない(横浜石井) Annen add end

    End Sub

    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ImportEvent()

        Dim msg As String = "取込処理"

        ' 請求先判定(TSMCか否か)
        Me._IsTsmc = Me.IsTsmc()
        ' TSMCか否かによる 取込項目の表示制御と初期値制御
        Me._G.SetControlsStatus2(Me._IsTsmc)

        '処理開始アクション
        Call Me.StartAction()
        '2011/08/09 菱刈 取込時にキャッシュから名称の取得
        If String.IsNullOrEmpty(Me._Frm.lblSeikyuNm.TextValue) = True Then

            'Dim SeqCd As ArrayList = Me._ControlG.GetSeqNm(Me._Frm.txtSeikyuCd.TextValue)
            '20160927 要番2622 tsunehira add Start
            Dim SeqCd As ArrayList = Me._ControlG.GetSeqNm(Me._Frm.cmbBr.SelectedValue.ToString, Me._Frm.txtSeikyuCd.TextValue)
            '20160927 要番2622 tsunehira add End

            If SeqCd.Count >= 1 Then
                Me._Frm.lblSeikyuNm.TextValue = SeqCd(0).ToString()

                '2011/08/28 須賀 名義(口座)名／全体値引率／値引額の取得 スタート
                Me._Frm.lblSikyuMeigi.TextValue = Me._G.GetKouzaMeigiNm(Me._Frm.imdInvDate.TextValue, SeqCd(2).ToString())

                If Me._KeisanFlg = False Then
                    Me._Frm.numNebikiRateK.Value = SeqCd(4).ToString()
                    Me._Frm.numNebikiGakuK.Value = SeqCd(5).ToString()
                End If
                '2011/08/28 須賀 名義(口座)名／全体値引率／値引額の取得 エンド
            End If
        End If


        '権限チェック
        If Me._V.IsAuthorityChk(LMG050C.EventShubetsu.IMPORT) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsImportChk() = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(msg) = False Then
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMG050DS()

        If String.IsNullOrEmpty(Me._Frm.lblSeikyuNo.TextValue) Then

            'ヘッダ部の項目格納
            Call Me.SetDataSetSave(ds)
            '★ ADD START 2011/09/06 SUGA
            '明細部の項目格納
            Call Me.SetDataSetSaveDtl(ds)
            '★ ADD E N D 2011/09/06 SUGA

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "ChkNewData")

            '==========================
            'WSAクラス呼出
            '==========================
            'MyBase.CallWSA("LMG050BLF", "ChkNewData", ds)
            ds = MyBase.CallWSA("LMG050BLF", "ChkNewData", ds)

            'メッセージコードの判定
            If MyBase.IsMessageExist = True Then
                MyBase.ShowMessage(Me._Frm)
                Call Me.EndAction() '終了処理
                Exit Sub
            End If

            MyBase.Logger.EndLog(MyBase.GetType.Name, "ChkNewData")

        End If

        '並び替え(レコードNoで並び替える)
        Me._Frm.sprSeikyuM.ActiveSheet.SortRows(LMG050G.sprSeikyuMDef.RECORD_NO.ColNo, True, False)

        '編集内容を保持する
        Call Me.SetDataSetAddDtl()

        ' ''以前取込データの削除
        Call Me.DeleteImportRow()

        'データ抽出プログラム起動
        '2014.08.21 修正START 多通貨対応
        'Dim rtnDs As DataSet = Me.OpenTorikomi()
        Dim rtnDs As DataSet = Me.OpenTorikomi(ds)
        '2014.08.21 修正END 多通貨対応

        '終了処理
        Call Me.EndAction()

        '取込開始日の設定
        Call Me._G.SetTorikomiStartDay(rtnDs.Tables(LMG900C.TABLE_NM_DATE))

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._KeiriModoshi_Flg, LMG050C.EventShubetsu.IMPORT)

        '取得データをSPREADに表示
        Call Me._G.SetSpread(Me._FindDs.Tables(LMG050C.TABLE_NM_DTL), _dsCombo)

        '計算処理を行う(率値引額も再計算する)
        Call Me.SetCalcAll(True)

        If Me._IsTsmc Then
            If Me._TsmcImpCnt = 0 Then
                MyBase.ShowMessage(Me._Frm, "G033", New String() {"取込対象データ", ""})
            Else
                MyBase.ShowMessage(Me._Frm, "G002", New String() {"取込処理", ""})
            End If
        Else
            '(2013.01.15)要望番号1763 -- START --
            ''返却されたメッセージを表示
            'Me.ShowMessage(Me._Frm)
            If MyBase.IsErrorMessageExist = False AndAlso rtnDs.Tables("LMG900OUT").Rows.Count.ToString.Equals("0") = True Then
                '取込データが１件もない場合
                MyBase.ShowMessage(Me._Frm, "G033", New String() {"取込対象データ", ""})
            Else
                '返却されたメッセージを表示
                Me.ShowMessage(Me._Frm)
            End If
            '(2013.01.15)要望番号1763 --  END  --
        End If

        'フォーカスの設定
        Call Me.SetFocus()

    End Sub

    ''' <summary>
    ''' 初期化処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeEvent()

        Select Case Me._Frm.cmbStateKbn.SelectedValue.ToString()
            Case LMG050C.STATE_KAKUTEI      '確定済
                'ステージアップ処理を行う
                Call Me.StageUp(LMG050C.EventShubetsu.INITIALIZE)

            Case LMG050C.STATE_INSATU_ZUMI  '印刷済
                '論理削除後、そのデータを元に新規登録処理を行う
                Call Me.InsertCopyKuro()

            Case LMG050C.STATE_KEIRI_TAISHO_GAI     '経理対象外
                '外部倉庫用ABP対策
                Dim drABP As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'G203' AND KBN_NM1 = '", LM.Base.LMUserInfoManager.GetNrsBrCd, "'"))
                If drABP.Length > 0 Then
                    'この営業所では(LMG040の)請求データ出力処理により経理取込対象外となるため、この進捗区分でも初期化は可能とする
                    'ステージアップ処理を行う
                    Call Me.StageUp(LMG050C.EventShubetsu.INITIALIZE)
                End If
        End Select

    End Sub

    ''' <summary>
    ''' 経理対象外処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub KeiriTaishoGaiEvent()

        Call Me.StageUp(LMG050C.EventShubetsu.KEIRITAISHOGAI)

    End Sub

    ''' <summary>
    ''' 経理戻し処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub KeiriModoshiEvent()

        Dim msg As String = "経理戻し処理"

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMG050C.EventShubetsu.KEIRIMODOSHI) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsCommonChk(msg) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        'DataSet設定
        Call Me.SetDataSetEventData(Me._FindDs)

        '処理続行確認
        Dim crtFlg As String = LMConst.FLG.OFF
        If Me._Frm.cmbSeiqtShubetu.SelectedValue.Equals(LMGControlC.CRT_TORIKOMI) Then
            '鑑作成区分 = "00"(自動取込請求書)且つ、進捗区分が03以上の未来データが存在する場合メッセージ内容が異なる

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectNextData")

            '==========================
            'WSAクラス呼出
            '==========================
            Dim msgDs As DataSet = MyBase.CallWSA("LMG050BLF", "SelectNextData", Me._FindDs)

            'ログ出力
            MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectNextData")

            If MyBase.IsMessageExist = True Then
                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(Me._Frm) = MsgBoxResult.Ok Then '「OK」を選択
                    crtFlg = LMConst.FLG.ON
                Else '「キャンセル」を選択
                    Call Me.EndAction() '終了処理
                    Exit Sub
                End If
            Else
                If Me.ConfirmMsg(msg) = False Then
                    Call Me.EndAction() '終了処理
                    Exit Sub
                End If
            End If

        Else
            If Me.ConfirmMsg(msg) = False Then
                Call Me.EndAction() '終了処理
                Exit Sub
            End If
        End If

        '鑑作成区分値設定用フラグ設定
        Me._FindDs.Tables(LMG050C.TABLE_NM_HED).Rows(0).Item("CRT_FLG") = crtFlg

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "InsertAkaKuro")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG050BLF", "InsertAkaKuro", Me._FindDs)
        Dim rtnHedDr As DataRow = rtnDs.Tables(LMG050C.TABLE_NM_HED).Rows(0)

        'メッセージコードの判定
        If MyBase.IsMessageExist = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "InsertAkaKuro")

        '終了処理
        Call Me.EndAction()

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(Com.Const.DispMode.EDIT, Com.Const.RecordStatus.NOMAL_REC)

        '請求番号再設定
        Me._PrmDs.Tables(LMG050C.TABLE_NM_IN).Rows(0).Item("SKYU_NO") = rtnHedDr.Item("SKYU_NO")

        '再検索処理を行う
        Call Me.SelectData()

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._KeiriModoshi_Flg, LMG050C.EventShubetsu.KEIRIMODOSHI)

        'メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G002", New String() {msg, ""})

        'フォーカスの設定
        Call Me.SetFocus()

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent(ByVal frm As LMG050F)

        'カーソル位置の設定
        Dim objNm As String = Me._Frm.ActiveControl.Name()

        '権限チェック
        If Me._V.IsAuthorityChk(LMG050C.EventShubetsu.MSTSANSHO) = False Then
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        'カーソル位置チェック
        If Me._V.IsFocusChk(objNm, LMG050C.EventShubetsu.MSTSANSHO) = False Then
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        '処理開始アクション
        If Not frm.sprSeikyuM.Name.Equals(objNm) Then
            'スプレッドの場合、後で独自に行うのでここはスルー
            Call Me.StartAction()
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True

        'ポップアップ処理
        Dim focusNoMove As Boolean = False
        Select Case objNm
            Case frm.sprSeikyuM.Name
                'スプレッド用照会
                Call Me.SetReturnSpreadPop(frm, objNm, LMG050C.EventShubetsu.MSTSANSHO)

                'フォーカス移動を抑制
                focusNoMove = True

            Case Else
                '請求先マスタ照会
                If Not Me.SetReturnSeiqtoPop(LMG050C.EventShubetsu.MSTSANSHO) Then
                    '強制終了となった場合は速やかに抜ける
                    Exit Sub
                End If
        End Select

        'メッセージエリアの設定
        Call Me.SetBaseMsg()

        '処理終了アクション
        Call Me.EndAction()

        'フォーカスの設定
        Call Me.SetFocus(focusNoMove)

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="eventShubetu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveEvent(ByVal eventShubetu As LMG050C.EventShubetsu) As Boolean

        Dim msg As String = "保存処理"

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMG050C.EventShubetsu.SAVE) = False Then
            Call Me.EndAction()  '終了処理
            Return False
        End If

        '単項目/関連チェック
        If Me._V.IsSaveChk() = False Then
            Call Me.EndAction()  '終了処理
            Return False
        End If

        '外部倉庫用ABP対策
        Dim drABP As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'G203' AND KBN_NM1 = '", LM.Base.LMUserInfoManager.GetNrsBrCd, "'"))
        If drABP.Length = 0 Then
            '真荷主の存在チェック
            If Me.TCustBpChk() = False Then
                Call Me.EndAction()  '終了処理
                Return False
            End If
        End If

        '最低保証額の再計算
        Dim ds As DataSet = New LMG050DS()
        Call Me.SetDataSetSave(ds)
        If Me.SelectSeiqtoData(ds) = False Then
            Call Me.EndAction()  '終了処理
            Return False
        End If

        '計算処理を行う(率値引額も再計算する)
        If Me.SetCalcAll(True) = False Then
            Call Me.EndAction()  '終了処理
            Return False
        End If

        '並び替え(レコードNoで並び替える)
        Me._Frm.sprSeikyuM.ActiveSheet.SortRows(LMG050G.sprSeikyuMDef.RECORD_NO.ColNo, True, False)

        'Select Case eventShubetu
        '    Case LMG050C.EventShubetsu.SAVE
        '        '処理続行確認
        '        If Me.ConfirmMsg(msg) = False Then
        '            Call Me.EndAction() '終了処理
        '            Return False
        '        End If
        'End Select

        'DataSet設定
        ds = New LMG050DS()
        'ヘッダ部の項目格納
        Call Me.SetDataSetSave(ds)
        '★ ADD START 2011/09/06 SUGA
        '明細部の項目格納
        Call Me.SetDataSetSaveDtl(ds, True)
        '★ ADD E N D 2011/09/06 SUGA

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG050BLF", "SaveData", ds)

        'メッセージコードの判定
        If MyBase.IsMessageExist = True Then

            Dim MessageId As String = GetMessageID()
            If MessageId.Equals("E265") = True Then
                Me._ControlV.SetErrorControl(Me._Frm.txtSeikyuCd)
            End If
            MyBase.ShowMessage(Me._Frm)
            Call Me.EndAction() '終了処理
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveData")

        '終了処理
        Call Me.EndAction()

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

        '請求番号再設定
        If String.IsNullOrEmpty(Me._Frm.lblSeikyuNo.TextValue) Then
            Dim skyuNo As String = rtnDs.Tables(LMG050C.TABLE_NM_HED).Rows(0).Item("SKYU_NO").ToString()
            Me._PrmDs.Tables(LMG050C.TABLE_NM_IN).Rows(0).Item("SKYU_NO") = skyuNo
        End If

#If True Then  'ADD 2018/09/19 経理戻しでのバグ　新規登録後、再表示時に@SYS_DEL_FLG未設定のためエラーになる対応
        If String.IsNullOrEmpty(Me._PrmDs.Tables(LMG050C.TABLE_NM_IN).Rows(0).Item("SYS_DEL_FLG").ToString) = True Then
            Me._PrmDs.Tables(LMG050C.TABLE_NM_IN).Rows(0).Item("SYS_DEL_FLG") = "0"
        End If
#End If
        '再検索処理を行う
        Call Me.SelectData()

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._KeiriModoshi_Flg)

        '2018/04/09 001225【LMS】請求鑑_全自動の場合は経理控を印刷しない(横浜石井) Annen add start
        Call Me._G.SetKeiriHikae()
        '2018/04/09 001225【LMS】請求鑑_全自動の場合は経理控を印刷しない(横浜石井) Annen add end

        'メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G002", New String() {msg, ""})

        'フォーカスの設定
        Call Me.SetFocus()

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(Me._Frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveEvent(LMG050C.EventShubetsu.CLOSE) = False Then

                    e.Cancel = True

                End If

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

        RemoveHandler Me._Frm.txtSeikyuCd.Leave, AddressOf txtSeikyuCd_Leave

        'カーソル位置の設定
        Dim objNm As String = Me._Frm.ActiveControl.Name()

        '入力不可の場合、処理終了
        If Me._Frm.ActiveControl.TabStop = False Then
            'Me._ControlG.SetNextControl(Me._Frm.sprSeikyuM)  'タブ移動処理
            Me.SetNextControlOnSeikyuCd(e)  'タブ移動処理
            Exit Sub
        End If

        '権限チェック
        If Me._V.IsAuthorityChk(LMG050C.EventShubetsu.ENTER) = False Then
            'Me._ControlG.SetNextControl(Me._Frm.sprSeikyuM)  'タブ移動処理
            Me.SetNextControlOnSeikyuCd(e)  'タブ移動処理
            Exit Sub
        End If

        'カーソル位置チェック
        If Not e Is Nothing AndAlso Me._V.IsFocusChk(objNm, LMG050C.EventShubetsu.ENTER) = False Then
            'Me._ControlG.SetNextControl(Me._Frm.sprSeikyuM)  'タブ移動処理
            Me.SetNextControlOnSeikyuCd(e)  'タブ移動処理
            Exit Sub
        End If

        Select Case objNm
            Case Me._Frm.sprSeikyuM.Name
                'スプレッド

                'Pop起動処理：画面表示なし
                Me._PopupSkipFlg = False

                'スプレッド用照会
                Call Me.SetReturnSpreadPop(Me._Frm, objNm, LMG050C.EventShubetsu.ENTER)

            Case Else
                '請求先

                '未入力の場合、処理終了
                If String.IsNullOrEmpty(Me._Frm.txtSeikyuCd.TextValue) Then
                    Me._Frm.lblSeikyuNm.TextValue = String.Empty
                    Me._Frm.lblSikyuMeigi.TextValue = String.Empty
                    'Me._ControlG.SetNextControl(Me._Frm.sprSeikyuM)  'タブ移動処理
                    Me.SetNextControlOnSeikyuCd(e)  'タブ移動処理
                    Exit Sub
                End If

                'Pop起動処理：画面表示なし
                Me._PopupSkipFlg = False

                If Not Me.SetReturnSeiqtoPop(LMG050C.EventShubetsu.ENTER) Then
                    '強制終了となった場合は速やかに抜ける
                    Exit Sub
                End If

                'タブ移動処理
                'Me._ControlG.SetNextControl(Me._Frm.sprSeikyuM)  'タブ移動処理
                Me.SetNextControlOnSeikyuCd(e)  'タブ移動処理

                'DataSet設定
                'Call Me.setCurrList()
        End Select

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    Private Sub SetNextControlOnSeikyuCd(ByVal e As System.Windows.Forms.KeyEventArgs)

        If e Is Nothing Then
            AddHandler Me._Frm.txtSeikyuCd.Leave, AddressOf txtSeikyuCd_Leave
            Exit Sub
        End If

        Me._ControlG.SetNextControl(Me._Frm.sprSeikyuM)  'タブ移動処理

        AddHandler Me._Frm.txtSeikyuCd.Leave, AddressOf txtSeikyuCd_Leave

    End Sub

    ''' <summary>
    ''' 行追加処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RowAdd()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMG050C.EventShubetsu.ADDROW) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsAddRowChk() = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '並び替え(レコードNoで並び替える)
        Me._Frm.sprSeikyuM.ActiveSheet.SortRows(LMG050G.sprSeikyuMDef.RECORD_NO.ColNo, True, False)

        'DataSet設定
        Call Me.SetDataSetAddDtl()

        'セグメント初期値取得
        Dim dsDef As DataSet = Me.GetDefSeg()

        'ポップアップ起動
        Me.SetReturnSeiqKmkPop(dsDef)

        '終了処理
        Call Me.EndAction()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(Me._FindDs.Tables(LMG050C.TABLE_NM_DTL), _dsCombo)

        '計算処理を行う(率値引額も再計算する)
        Call Me.SetCalcAll(True)

        'メッセージエリアの設定
        Call Me.SetBaseMsg()

        'フォーカスの設定
        Call Me.SetFocus()

    End Sub

    ''' <summary>
    ''' 行削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RowDel()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMG050C.EventShubetsu.DELETEROW) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '並び替え(レコードNoで並び替える)
        Me._Frm.sprSeikyuM.ActiveSheet.SortRows(LMG050G.sprSeikyuMDef.RECORD_NO.ColNo, True, False)

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(Me._Frm.sprSeikyuM.ActiveSheet, LMG050G.sprSeikyuMDef.DEF.ColNo)

        '単項目/関連チェック
        If Me._V.IsDelRowChk(list) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '明細行格納DataTableの編集
        Call Me.SetDataDeleateRow(list)

        '終了処理
        Call Me.EndAction()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(Me._FindDs.Tables(LMG050C.TABLE_NM_DTL), _dsCombo)

        '計算処理を行う(率値引額も再計算する)
        Call Me.SetCalcAll(True)

        'メッセージエリアの設定
        Call Me.SetBaseMsg()

        'フォーカスの設定
        Call Me.SetFocus()

    End Sub

    ''' <summary>
    ''' SAP出力処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnSapOutClick()

        Dim msg As String = Me._Frm.btnSapOut.Text & "処理"

        ' 処理開始アクション
        Call Me.StartAction()

        ' 権限チェック
        If Me._V.IsAuthorityChk(LMG050C.EventShubetsu.SAPOUT) = False Then
            Call Me.EndAction()  ' 終了処理
            Exit Sub
        End If

        ' 入力チェック
        If Me._V.IsSapOutChk() = False Then
            Call Me.EndAction()
            Exit Sub
        End If

        ' 処理続行確認
        If Me.ConfirmMsg(String.Concat(msg, "を実行")) = False Then
            Call Me.EndAction() ' 終了処理
            Exit Sub
        End If

        ' DataSet, DataTable 設定
        Dim ds As DataSet = New LMG050DS()
        Dim dt As DataTable = ds.Tables(LMG050C.TABLE_NM_HED)
        Dim dr As DataRow = dt.NewRow()
        With Me._Frm
            ' PKey
            dr.Item("SKYU_NO") = .lblSeikyuNo.TextValue
            ' 更新する進捗区分の値
            dr.Item("STATE_KB") = LMG050C.STATE_KEIRI_TORIKOMI_ZUMI
            ' 営業所コード
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            ' 排他制御用
            dr.Item("SYS_UPD_DATE") = .lblSysUpdDate.TextValue
            dr.Item("SYS_UPD_TIME") = .lblSysUpdTime.TextValue
        End With
        dt.Rows.Add(dr)

        ' ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SapOut")

        '==========================
        ' WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG050BLF", "SapOut", ds)

        ' メッセージコードの判定
        If MyBase.IsMessageExist = True Then

            Dim MessageId As String = GetMessageID()
            MyBase.ShowMessage(Me._Frm)
            Call Me.EndAction() ' 終了処理
            Return
        End If

        ' ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SapOut")

        ' 終了処理
        Call Me.EndAction()

        ' 再検索処理を行う
        Call Me.SelectData()

        ' 画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._KeiriModoshi_Flg)

        ' 更新成功時、メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G002", New String() {msg, ""})

    End Sub

    ''' <summary>
    ''' SAP取消処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnSapCancelClick()

        Dim msg As String = Me._Frm.btnSapCancel.Text & "処理"

        ' 処理開始アクション
        Call Me.StartAction()

        ' 権限チェック
        If Me._V.IsAuthorityChk(LMG050C.EventShubetsu.SAPCANCEL) = False Then
            Call Me.EndAction() ' 終了処理
            Exit Sub
        End If

        ' 入力チェック
        If Me._V.IsSapCancelChk() = False Then
            Call Me.EndAction()
            Exit Sub
        End If

        ' 処理続行確認
        If Me.ConfirmMsg(String.Concat(msg, "を実行")) = False Then
            Call Me.EndAction() ' 終了処理
            Exit Sub
        End If

        ' DataSet, DataTable 設定
        Dim ds As DataSet = New LMG050DS()
        Dim dt As DataTable = ds.Tables(LMG050C.TABLE_NM_HED)
        Dim dr As DataRow = dt.NewRow()
        With Me._Frm
            ' PKey
            dr.Item("SKYU_NO") = .lblSeikyuNo.TextValue
            ' 更新する進捗区分の値
            dr.Item("STATE_KB") = LMG050C.STATE_INSATU_ZUMI
            ' 営業所コード
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            ' 排他制御用
            dr.Item("SYS_UPD_DATE") = .lblSysUpdDate.TextValue
            dr.Item("SYS_UPD_TIME") = .lblSysUpdTime.TextValue
        End With
        dt.Rows.Add(dr)

        ' ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SapCancel")

        '==========================
        ' WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG050BLF", "SapCancel", ds)

        ' メッセージコードの判定
        If MyBase.IsMessageExist = True Then

            Dim MessageId As String = GetMessageID()
            MyBase.ShowMessage(Me._Frm)
            Call Me.EndAction() ' 終了処理
            Return
        End If

        ' ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SapCancel")

        ' 終了処理
        Call Me.EndAction()

        ' 再検索処理を行う
        Call Me.SelectData()

        ' 画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._KeiriModoshi_Flg)

        ' 更新成功時、メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G002", New String() {msg, ""})

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnPrintClick()

        Dim msg As String = "印刷処理"

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMG050C.EventShubetsu.PRINT) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '入力チェック
        If Me._V.IsPrintChk() = False Then
            Call Me.EndAction()
            Exit Sub
        End If

        '赤伝票の場合、ワーニングメッセージ表示
        If Me._Frm.chkAkaden.GetBinaryValue().Equals(LMConst.FLG.ON) Then
            If MyBase.ShowMessage(Me._Frm, "W140", New String() {"印刷対象データ", "赤データ"}) = MsgBoxResult.Cancel Then
                Call Me.SetBaseMsg() 'メッセージの設定
                Call Me.EndAction() '終了処理
                Exit Sub
            End If
        Else
            If Me._KeiriModoshi_Flg = True Then
                If MyBase.ShowMessage(Me._Frm, "W140", New String() {"印刷対象データ", "経理戻しが行われたデータ"}) = MsgBoxResult.Cancel Then
                    Call Me.SetBaseMsg() 'メッセージの設定
                    Call Me.EndAction() '終了処理
                    Exit Sub
                End If

            End If
        End If

        '(2013.01.08)要望番号1742 請求鑑だけでなく、一括印刷時もステージ更新
        'If Me._Frm.cmbStateKbn.SelectedValue.Equals(LMG050C.STATE_KAKUTEI) _
        'AndAlso Me._Frm.cmbPrint.SelectedValue.Equals(LMG050C.PRINT_SEIKYU_SHO) Then
        If Me._Frm.cmbStateKbn.SelectedValue.Equals(LMG050C.STATE_KAKUTEI) _
        AndAlso (Me._Frm.cmbPrint.SelectedValue.Equals(LMG050C.PRINT_SEIKYU_SHO) _
                 Or Me._Frm.cmbPrint.SelectedValue.Equals(LMG050C.PRINT_IKKATSU)) Then

            'ステージ更新処理
            Dim megId As String = Me.UpdateStatus(LMG050C.EventShubetsu.PRINT)
            If String.IsNullOrEmpty(megId) = False _
            AndAlso megId <> "E078" Then
                Call Me.EndAction() '終了処理
                Exit Sub
            End If
        End If

        '印刷処理を行う
        If Me.PrintKagami() = False Then
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me.EndAction()

        '再検索処理を行う
        Call Me.SelectData()

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._KeiriModoshi_Flg)

        'メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G002", New String() {msg, ""})

    End Sub
    '★ UPD START 2011/09/06 SUGA

    'End Sub
    ''' <summary>
    ''' スプレッドのセルLeave処理
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LeaveSprCell(ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        If Me._Frm.sprSeikyuM.ActiveSheet.Cells(e.Row, e.Column).Locked = True Then
            Exit Sub
        End If

        Select Case e.Column
            Case LMG050G.sprSeikyuMDef.KEISAN_GAKU.ColNo _
                , LMG050G.sprSeikyuMDef.NEBIKI_RATE.ColNo _
                , LMG050G.sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo

                Dim errFlg As Boolean = False

                '処理開始アクション
                Call Me.StartAction()

                '明細部の計算処理を行う
                If Me.SprCalc(e.Row, e.Column) = False Then
                    errFlg = True
                End If

                '集計部の計算処理を行う
                If Me.HedCalc(Me._ControlV.GetCellValue(Me._Frm.sprSeikyuM.ActiveSheet.Cells(e.Row, LMG050G.sprSeikyuMDef.KAZEI_KBN.ColNo))
                              ) = False Then
                    errFlg = True
                End If

                '終了処理
                Call Me.EndAction()

                If errFlg = False Then
                    'メッセージ設定
                    Call Me.SetBaseMsg()
                End If

            Case LMG050G.sprSeikyuMDef.BUSYO.ColNo

                '勘定科目コード設定
                Me._Frm.sprSeikyuM.SetCellValue(e.Row, LMG050G.sprSeikyuMDef.KANJOKMK_CD.ColNo, Me.SetKanjoKmkCd(e.Row))

            Case LMG050G.sprSeikyuMDef.TCUST_BPCD.ColNo

                'Pop起動処理：画面表示なし
                Me._PopupSkipFlg = False

                '真荷主
                Call Me.SetReturnSpreadPop(Me._Frm, Me._Frm.sprSeikyuM.Name, LMG050C.EventShubetsu.ENTER)

        End Select

    End Sub
    '★ UPD E N D 2011/09/06 SUGA

    ''' <summary>
    ''' 集計部Leaveイベント
    ''' </summary>
    ''' <param name="taxKbn"></param>
    ''' <remarks></remarks>
    Private Sub LeaveHedCalc(ByVal taxKbn As String)

        If Me._Frm.lblSituation.DispMode.Equals(DispMode.VIEW) Then
            Exit Sub
        End If

        '処理開始アクション
        Call Me.StartAction()

        If Me.HedCalc(taxKbn) = False Then
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me.EndAction()

        'メッセージ設定
        Call Me.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 請求先コードLeaveイベント
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LeavetxtSeikyuCd()

        If Me._SeikyuCd.Equals(Me._Frm.txtSeikyuCd.TextValue) = True Then
            Exit Sub
        End If

        Me.EnterAction(Nothing)

        ' 請求先判定(TSMCか否か)
        Me._IsTsmc = Me.IsTsmc()
        ' TSMCか否かによる 取込項目の表示制御と初期値制御
        Me._G.SetControlsStatus2(Me._IsTsmc)

    End Sub

    ''' <summary>
    ''' 請求先コードGotFocusイベント
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GotFocustxtSeikyuCd()

        Me._SeikyuCd = Me._Frm.txtSeikyuCd.TextValue

    End Sub

    '2014.08.21 追加START 多通貨対応
    ''' <summary>
    ''' 請求建値ロストフォーカス処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ControlLeave()

        '参照モードの場合処理終了
        If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True OrElse
           DispMode.INIT.Equals(Me._Frm.lblSituation.DispMode) = True Then
            Exit Sub
        End If

        '画面制御
        Me._G.SetControlRate()

    End Sub

    ''' <summary>
    ''' 請求通貨＋契約通貨　EX_RATE取得処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function setCurrList() As Boolean

        Dim ds As DataSet = New LMG050DS()

        Dim dt As DataTable = ds.Tables(LMG050C.TABLE_NM_HED)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm

            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("SEIQTO_CD") = .txtSeikyuCd.TextValue

        End With

        dt.Rows.Add(dr)

        '==========================
        'WSAクラス呼出
        '==========================
        ds = MyBase.CallWSA("LMG050BLF", "setCurrList", ds)

        'メッセージコードの判定
        If MyBase.IsMessageExist = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me.EndAction() '終了処理
            Return True
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "setCurrList")

        'コンボコントロールの設定
        Call Me._G.SetComboControl(ds)
        '2014.08.21 追加END 多通貨対応

        Dim dtCurr As DataTable = ds.Tables("LMG050_CURRINFO")

        If dtCurr.Rows.Count > 0 Then
            If String.IsNullOrEmpty((dtCurr.Rows(0).Item("CUR_RATE").ToString)) Then
                'レートが取得できなければエラーとし、本処理を終了する（画面も出さない）
                MyBase.ShowMessage(Me._Frm, "E02K")
                Return False
            End If

            Me._Frm.cmbSeiqCurrCd.SelectedValue = dtCurr.Rows(0).Item("SEIQ_CURR_CD").ToString()
            Me._Frm.cmbCurrencyConversion1.SelectedValue = dtCurr.Rows(0).Item("SEIQ_CURR_CD").ToString()
            Me._Frm.numExRate.Value = Convert.ToDecimal(dtCurr.Rows(0).Item("CUR_RATE"))
            Me._Frm.cmbCurrencyConversion2.SelectedValue = dtCurr.Rows(0).Item("ITEM_CURR_CD").ToString()
        Else
            Me._Frm.cmbSeiqCurrCd.SelectedValue = "JPY"
            Me._Frm.cmbCurrencyConversion1.SelectedValue = "JPY"
            Me._Frm.numExRate.Value = 1.0
            Me._Frm.cmbCurrencyConversion2.SelectedValue = "JPY"
        End If

        Return True

    End Function

    '2014.08.21 追加END 多通貨対応

    ''' <summary>
    ''' コンボボックス用の値取得
    ''' </summary>
    Private Sub GetComboData()

        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)

        _dsCombo = New LMG050DS()

        '製品セグメント取得
        Dim dt As DataTable = _dsCombo.Tables("LMG050COMBO_SEIHINA")
        Dim dr As DataRow = dt.NewRow()
        dr.Item("KBN_LANG") = If("0".Equals(lgm.MessageLanguage()), "ja", "en")
        dt.Rows.Add(dr)
        _dsCombo = MyBase.CallWSA("LMG050BLF", "SelectComboSeihin", _dsCombo)

        '地域セグメント取得
        dt = _dsCombo.Tables("LMG050COMBO_CHIIKI")
        dr = dt.NewRow()
        dr.Item("KBN_LANG") = If("0".Equals(lgm.MessageLanguage()), "ja", "en")
        dt.Rows.Add(dr)
        _dsCombo = MyBase.CallWSA("LMG050BLF", "SelectComboChiiki", _dsCombo)

    End Sub

    ''' <summary>
    ''' セグメント初期値取得
    ''' </summary>
    Private Function GetDefSeg() As DataSet

        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)

        Dim ds As DataSet = New LMG050DS()
        Dim dt As DataTable = ds.Tables("LMG050DEF_SEG")
        Dim dr As DataRow = dt.NewRow()
        dr.Item("NRS_BR_CD") = _Frm.cmbBr.SelectedValue
        dr.Item("SEIQTO_CD") = _Frm.txtSeikyuCd.TextValue
        dr.Item("KBN_LANG") = If("0".Equals(lgm.MessageLanguage()), "ja", "en")
        dt.Rows.Add(dr)

        Return MyBase.CallWSA("LMG050BLF", "SelectDefSeg", ds)

    End Function

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StartAction()

        '画面全ロック
        MyBase.LockedControls(Me._Frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(Me._Frm)

        '背景色クリア
        Me._ControlG.SetBackColor(Me._Frm)

        'イベント解除
        Call Me.RemoveEventHandler()

    End Sub

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EndAction()

        '画面解除
        MyBase.UnLockedControls(Me._Frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

        'イベント設定
        Call Me.SetEventHandler()

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectData()

        'SPREAD(表示行)初期化
        Me._Frm.sprSeikyuM.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG050BLF", "SelectData", Me._PrmDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '明細データ格納(更新時使用)
        Me._FindDs = rtnDs
        Dim dtDtl As DataTable = Me._FindDs.Tables(LMG050C.TABLE_NM_DTL)
        Dim drHed As DataRow = Me._FindDs.Tables(LMG050C.TABLE_NM_HED).Rows(0)

        '2014.08.21 追加START 多通貨対応
        'コンボコントロールの設定
        Call Me._G.SetComboControl(rtnDs)
        '2014.08.21 追加END 多通貨対応

        'ヘッダ部に値設定
        Call Me._G.SetHed(Me._FindDs)

        '経理戻しを行ったデータの場合、True
        If String.IsNullOrEmpty(drHed.Item("AKA_SKYU_NO").ToString()) = False Then
            Me._KeiriModoshi_Flg = True
        End If

        Me._EdaNo = Convert.ToInt32(drHed.Item("MAX_SKYU_SUB_NO").ToString())


        If dtDtl.Rows.Count > 0 Then

            Dim max As Integer = dtDtl.Rows.Count - 1
            For i As Integer = 0 To max
                dtDtl.Rows(i).Item("RECORD_NO") = i.ToString().PadLeft(2, Convert.ToChar("0"))
                dtDtl.Rows(i).Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                dtDtl.Rows(i).Item("INS_FLG") = LMConst.FLG.OFF
                dtDtl.Rows(i).Item("NRS_BR_CD") = drHed.Item("NRS_BR_CD")
            Next

            '取得データをSPREADに表示
            Call Me._G.SetSpread(dtDtl, _dsCombo)

        End If

        '計算処理を行う
        Call Me.SetCalcAll()

    End Sub

    ''' <summary>
    ''' ステージアップ処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StageUp(ByVal eventShubetu As LMG050C.EventShubetsu)

        Dim msg As String = String.Empty
        Select Case eventShubetu
            Case LMG050C.EventShubetsu.KAKUTEI
                msg = "確定処理"
            Case LMG050C.EventShubetsu.INITIALIZE
                msg = "初期化処理"
            Case LMG050C.EventShubetsu.KEIRITAISHOGAI
                msg = "経理対象外処理"
        End Select

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(eventShubetu) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsCommonChk(msg) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(msg) = False Then
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        'ステージ更新処理
        Dim msgId As String = Me.UpdateStatus(eventShubetu)
        If String.IsNullOrEmpty(msgId) = False Then
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me.EndAction()

        Select Case eventShubetu
            Case LMG050C.EventShubetsu.INITIALIZE
                'モード・ステータスの設定
                Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        End Select

        '再検索処理を行う
        Call Me.SelectData()

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._KeiriModoshi_Flg)

        'メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G002", New String() {msg, ""})

        'フォーカスの設定
        Call Me.SetFocus()

    End Sub

    ''' <summary>
    ''' ステージアップ更新処理
    ''' </summary>
    ''' <param name="eventShubetu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateStatus(ByVal eventShubetu As LMG050C.EventShubetsu) As String

        Dim rtnStr As String = String.Empty

        'DataSet設定
        Dim ds As DataSet = New LMG050DS()
        Call Me.SetDataSetStageUp(ds, eventShubetu)

        Dim rtnDs As DataSet = Nothing
        Dim method As String = String.Empty

        If eventShubetu.Equals(LMG050C.EventShubetsu.KAKUTEI) _
        AndAlso Me._Frm.cmbSeiqtShubetu.SelectedValue.ToString().Equals(LMGControlC.CRT_TORIKOMI) Then
            method = "UpKakuteiHed"
        ElseIf eventShubetu.Equals(LMG050C.EventShubetsu.INITIALIZE) _
        AndAlso Me._Frm.cmbSeiqtShubetu.SelectedValue.ToString().Equals(LMGControlC.CRT_TORIKOMI) Then
            method = "UpKakuteiHed"
        Else
            method = "UpStageKagamiHed"
        End If

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, method)

        '==========================
        'WSAクラス呼出
        '==========================
        rtnDs = MyBase.CallWSA("LMG050BLF", method, ds)

        'メッセージコードの判定
        If MyBase.IsMessageExist = True Then
            rtnStr = MyBase.GetMessageID()
            If MyBase.IsMessageExist() = True Then
                MyBase.ShowMessage(Me._Frm)
            End If
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, method)

        Return rtnStr

    End Function

    ''' <summary>
    ''' 取込バッチを起動する
    ''' </summary>
    ''' <remarks></remarks>
    Private Function OpenTorikomi(ByVal ds As DataSet) As DataSet

        If Me._IsTsmc Then
            ' 取込処理(TSMC)
            Return TorikomiTsmc(ds)
        End If

        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)

        'パラメータ生成
        Dim prm As LMFormData = New LMFormData()
        Dim prmDs As DataSet = New LMG900DS()
        Dim prmDt As DataTable = prmDs.Tables("LMG900IN")
        Dim prmDr As DataRow = prmDt.NewRow()

        With Me._Frm

            prmDr.Item("SKYU_NO") = .lblSeikyuNo.TextValue
            prmDr.Item("SEIQTO_CD") = .txtSeikyuCd.TextValue
            prmDr.Item("SKYU_DATE") = .imdInvDate.TextValue
            prmDr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            prmDr.Item("HOKAN_FLG") = .chkHokan.GetBinaryValue()
            prmDr.Item("NIYAKU_FLG") = .chkNiyaku.GetBinaryValue()
            prmDr.Item("UNCHIN_FLG") = .chkUnchin.GetBinaryValue()
            prmDr.Item("SAGYO_FLG") = .chkSagyou.GetBinaryValue()
            prmDr.Item("YOKOMOCHI_FLG") = .chkYokomochi.GetBinaryValue()
            prmDr.Item("TEMPLATE_FLG") = .chkTemplate.GetBinaryValue()
            prmDr.Item("BUSYO_CD") = Me.GetLoginUserBusyoCd()
            If String.IsNullOrEmpty(Me._Frm.lblSeikyuNoRelated.TextValue) = False Then
                prmDr.Item("UNC_SKYU_DATE_FROM") = .lblUnchinImpDate.TextValue
                prmDr.Item("SAG_SKYU_DATE_FROM") = .lblSagyoImpDate.TextValue
                prmDr.Item("YOK_SKYU_DATE_FROM") = .lblYokomochiImpDate.TextValue
            End If
            prmDr.Item("KBN_LANG") = If("0".Equals(lgm.MessageLanguage()), "ja", "en")
        End With

        prmDt.Rows.Add(prmDr)
        prm.ParamDataSet = prmDs

        '請求取込データ抽出作成(LMG900)を開く
        LMFormNavigate.NextFormNavigate(Me, "LMG900", prm)

        '******* 返却パラメータの設定 *******
        If prm.ReturnFlg = True Then

            '(2013.01.15)要望番号1763 -- START --
            Dim RowMax As Integer = prm.ParamDataSet.Tables("LMG900OUT").Rows.Count
            Dim RowCnt As Integer = 0

            '鑑最低保証レコードに真荷主・セグメントをセットする
            'セットする値は行追加時の初期値と同じ
            Dim dsDef As DataSet = Me.GetDefSeg()
            If dsDef.Tables("LMG050DEF_SEG").Rows.Count > 0 Then
                For i As Integer = 0 To RowMax - 1
                    Dim dt As DataTable = prm.ParamDataSet.Tables("LMG900OUT")
                    If "01".Equals(dt.Rows(i).Item("GROUP_KB").ToString) AndAlso "05".Equals(dt.Rows(i).Item("SEIQKMK_CD").ToString) Then
                        dt.Rows(i).Item("PRODUCT_SEG_CD") = dsDef.Tables("LMG050DEF_SEG").Rows(0).Item("DEF_SEG_SEIHIN").ToString()
                        dt.Rows(i).Item("ORIG_SEG_CD") = dsDef.Tables("LMG050DEF_SEG").Rows(0).Item("DEF_SEG_CHIIKI").ToString()
                        dt.Rows(i).Item("DEST_SEG_CD") = dsDef.Tables("LMG050DEF_SEG").Rows(0).Item("DEF_SEG_CHIIKI").ToString()
                        dt.Rows(i).Item("TCUST_BPCD") = dsDef.Tables("LMG050DEF_SEG").Rows(0).Item("TCUST_BPCD").ToString()
                        dt.Rows(i).Item("TCUST_BPNM") = dsDef.Tables("LMG050DEF_SEG").Rows(0).Item("TCUST_BPNM").ToString()
                    End If
                Next
            End If

            'ここまでで地域セグメントが設定されなかったレコードに、専用のコードをセットする
            Const CODE As String = "ZZZYYY"
            For i As Integer = 0 To RowMax - 1
                Dim dt As DataTable = prm.ParamDataSet.Tables("LMG900OUT")
                If String.IsNullOrEmpty(dt.Rows(i).Item("ORIG_SEG_CD").ToString) Then
                    dt.Rows(i).Item("ORIG_SEG_CD") = CODE
                End If
                If String.IsNullOrEmpty(dt.Rows(i).Item("DEST_SEG_CD").ToString) Then
                    dt.Rows(i).Item("DEST_SEG_CD") = CODE
                End If
            Next

            '計算結果が￥0の場合は、明細行を削除する
            For i As Integer = 0 To RowMax - 1
                If prm.ParamDataSet.Tables("LMG900OUT").Rows(RowCnt).Item("KEISAN_TLGK").ToString.Equals("0") = True Then
                    prm.ParamDataSet.Tables("LMG900OUT").Rows(RowCnt).Delete()
                Else
                    RowCnt = RowCnt + 1
                End If
            Next
            '(2013.01.15)要望番号1763 --  END  --

            Dim rtnDt As DataTable = prm.ParamDataSet.Tables("LMG900OUT")
            Dim setDt As DataTable = Me._FindDs.Tables(LMG050C.TABLE_NM_DTL)
            Dim importCnt As Integer = rtnDt.Rows.Count
            Dim max As Integer = importCnt - 1

            Dim setDr As DataRow

            '最大枝番を最新にする
            Me._Frm.lblMaxEdaban.TextValue = (Convert.ToInt32(Me._Frm.lblMaxEdaban.TextValue) + importCnt).ToString()

            'レコードNoの設定
            Dim recNo As Integer = setDt.Rows.Count

            '取込レコードに鑑最低保証レコードがあるか？
            Dim rtnRow As DataRow() = rtnDt.Select("GROUP_KB = '01' AND SEIQKMK_CD = '05'")
            Dim setRow As DataRow() = setDt.Select("GROUP_KB = '01' AND SEIQKMK_CD = '05' AND SYS_DEL_FLG = '0'")

            If rtnRow.Length > 0 AndAlso
                setRow.Length > 0 Then
                rtnRow(0).Delete()
            End If

            For i As Integer = 0 To rtnDt.Rows.Count - 1
                setDt.ImportRow(rtnDt.Rows(i))
            Next

            '2014.08.21 修正START 多通貨対応
            For j As Integer = 0 To setDt.Rows.Count - 1

                setDr = setDt.Rows(j)
                If ds.Tables("LMG050_CURRINFO").Rows.Count > 0 Then
                    setDr("ROUND_POS") = Convert.ToInt32(ds.Tables("LMG050_CURRINFO").Rows(0).Item("ROUND_POS"))
                    setDr("ITEM_CURR_CD") = ds.Tables("LMG050_CURRINFO").Rows(0).Item("ITEM_CURR_CD").ToString()
                Else
                    setDr("ROUND_POS") = 0
                    setDr("ITEM_CURR_CD") = "JPY"
                End If

            Next
            '2014.08.21 修正END 多通貨対応

            max = setDt.Rows.Count - 1
            For i As Integer = 0 To max
                If String.IsNullOrEmpty(setDt.Rows(i).Item("RECORD_NO").ToString()) Then
                    setDt.Rows(i).Item("RECORD_NO") = recNo.ToString().PadLeft(2, Convert.ToChar("0"))
                    recNo = recNo + 1
                End If
            Next

        End If

        Return prm.ParamDataSet

    End Function

    ''' <summary>
    ''' 取込処理(TSMC)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function TorikomiTsmc(ByVal ds As DataSet) As DataSet

        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)

        Dim prm As LMFormData = New LMFormData()

        ' 自営業所の部署コード(KBN_GROUP_CD IN ('B008', 'B007') 勘定科目導出用) の取得
        Dim bushoCdSet As New HashSet(Of String)
        Dim whereKbnB008 As String =
            String.Concat(
                "    KBN_GROUP_CD = 'B008'",
                "AND KBN_NM4 = '", LM.Base.LMUserInfoManager.GetNrsBrCd().ToString(), "'",
                "AND SYS_DEL_FLG = '0'")
        Dim drKbnB008 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(whereKbnB008)
        For Each drKbn As DataRow In drKbnB008
            If Not bushoCdSet.Contains(drKbn.Item("KBN_NM1").ToString()) Then
                bushoCdSet.Add(drKbn.Item("KBN_NM1").ToString())
            End If
        Next
        Dim whereKbnB007 As String =
            String.Concat(
                "    KBN_GROUP_CD = 'B007'",
                "AND KBN_NM2 = '", LM.Base.LMUserInfoManager.GetNrsBrCd().ToString(), "'",
                "AND KBN_NM3 = '1'",
                "AND SYS_DEL_FLG = '0'")
        Dim drKbnB007 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(whereKbnB007)
        For Each drKbn As DataRow In drKbnB007
            If Not bushoCdSet.Contains(drKbn.Item("KBN_CD").ToString()) Then
                bushoCdSet.Add(drKbn.Item("KBN_CD").ToString())
            End If
        Next

        Dim inDs As DataSet = New LMG050DS()
        Dim inDt As DataTable = inDs.Tables(LMG050C.TABLE_NM_IN_TSMC)
        Dim inDr As DataRow

        ' 抽出条件の DataSet, DataTable, DataRow の作成
        ' --
        ' 自営業所の部署コード(KBN_GROUP_CD IN ('B008', 'B007') 勘定科目導出用) 1件につき、
        ' 以下の各組み合わせの DataRow を作成する。
        '
        ' セット料金計算日数区分 = '90' 90日セット
        '     請求グループコード区分 = 保管料
        '     請求グループコード区分 = 荷役料
        '     請求グループコード区分 = 運賃
        ' セット料金計算日数区分 = '45' 45日セット
        '     請求グループコード区分 = デポ保管
        '     請求グループコード区分 = デポリフト
        '     請求グループコード区分 = 運送収入（ISO自車）
        For Each busyoCd As String In bushoCdSet
            For setClcDateIdx As Integer = 1 To 2
                For groupKbIdx As Integer = 1 To 3
                    With Me._Frm
                        inDr = inDt.NewRow

                        ' 営業所コード
                        inDr.Item("NRS_BR_CD") = .cmbBr.SelectedValue.ToString()
                        inDr.Item("BUSYO_CD") = busyoCd
                        ' 課税区分
                        inDr.Item("TAX_KB") = LMGControlC.TAX_KAZEI ' 課税
                        ' 請求グループコード区分
                        Select Case groupKbIdx
                            Case 1
                                ' 設定値:“保管料”または“デポ保管”の区分値
                                inDr.Item("GROUP_KB") = If(setClcDateIdx = 1, LMG050C.SKYU_GROUP_HOKAN, LMG050C.SKYU_GROUP_DEPOT_HOKAN)
                            Case 2
                                ' 設定値:“荷役料”または“デポリフト”の区分値
                                inDr.Item("GROUP_KB") = If(setClcDateIdx = 1, LMG050C.SKYU_GROUP_NIYAKU, LMG050C.SKYU_GROUP_DEPOT_LIFT)
                            Case 3
                                ' 設定値: “運賃”または“運送収入（ISO自車）”の区分値
                                inDr.Item("GROUP_KB") = If(setClcDateIdx = 1, LMG050C.SKYU_GROUP_UNCHIN, LMG050C.SKYU_GROUP_CONTAINER_UNSO)
                        End Select
                        ' 作成種別
                        inDr.Item("MAKE_SYU_KB") = LMGControlC.DETAIL_SAKUSEI_AUTO  ' 自動
                        ' 請求先コード
                        inDr.Item("SEIQTO_CD") = .txtSeikyuCd.TextValue()
                        ' セット料金計算日数区分
                        inDr.Item("UNIT_KB") = If(setClcDateIdx = 1, "90", "45")
                        ' 請求先日
                        inDr.Item("SKYU_DATE") = .imdInvDate.TextValue()
                        ' 区分の言語(ABM_DB)
                        inDr.Item("KBN_LANG") = If("0".Equals(lgm.MessageLanguage()), "ja", "en")

                        inDt.Rows.Add(inDr)
                    End With
                Next
            Next
        Next

        ' TSMC請求明細よりの取込データの取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG050BLF", "SelectTorikomiDataTsmc", inDs)

        Me._TsmcImpCnt = rtnDs.Tables(LMG050C.TABLE_NM_DTL).Rows().Count()
        If Me._TsmcImpCnt Mod 3 = 0 Then
            ' 金額配賦および請求番号設定
            Dim setOverAmo As Decimal = 0
            Dim gkTtl As Decimal = 0
            Dim gk(Me._TsmcImpCnt - 1) As Decimal
            Dim rate As Decimal = 0
            Dim kamokuCnt As Integer = 0    ' 勘定科目3種類のカウント
            For i As Integer = 0 To Me._TsmcImpCnt - 1
                kamokuCnt += 1
                If kamokuCnt = 4 Then
                    kamokuCnt = 1
                End If
                If kamokuCnt = 1 Then
                    If Not Decimal.TryParse(rtnDs.Tables(LMG050C.TABLE_NM_DTL).Rows(i).Item("SET_OVER_AMO").ToString(), setOverAmo) Then
                        setOverAmo = 0
                    End If
                    If Not Decimal.TryParse(rtnDs.Tables(LMG050C.TABLE_NM_DTL).Rows(i).Item("KEISAN_TLGK").ToString(), gkTtl) Then
                        gkTtl = 0
                    End If
                End If
                Dim unitKb As String = rtnDs.Tables(LMG050C.TABLE_NM_DTL).Rows(i).Item("UNIT_KB").ToString()
                rate = 0D
                Select Case kamokuCnt
                    Case 1
                        ' 90日セット 保管収入(国内)
                        ' 45日セット デポ保管収入
                        Continue For
                    Case 2
                        If unitKb = "90" Then
                            ' 90日セット 荷役収入(国内・入出庫)
                            rate = 0.33D
                        ElseIf unitKb = "45" Then
                            ' 45日セット デポリフト収入
                            rate = 0.13D
                        End If
                    Case 3
                        If unitKb = "90" Then
                            ' 90日セット 運送収入(自車)
                            rate = 0.22D
                        ElseIf unitKb = "45" Then
                            ' 45日セット 運送収入（ISO自車）
                            rate = 0.42D
                        End If
                End Select
                gk(i) = Math.Round((gkTtl * rate), 0, MidpointRounding.AwayFromZero)
                If kamokuCnt = 3 Then
                    gk(i - 2) = gkTtl - (gk(i - 1) + gk(i)) + setOverAmo
                End If
            Next
            For i As Integer = 0 To Me._TsmcImpCnt - 1
                rtnDs.Tables(LMG050C.TABLE_NM_DTL).Rows(i).Item("SKYU_NO") = Me._Frm.lblSeikyuNo.TextValue()
                rtnDs.Tables(LMG050C.TABLE_NM_DTL).Rows(i).Item("KEISAN_TLGK") = gk(i).ToString()
            Next
        End If

        prm.ParamDataSet = New LMG900DS()
        ' 経理戻しにより作成されたデータ(新黒)以外の場合の適用開始日の取得
        ' (TSMC 以外の取込では、LMG900H SetStartDate() にて行う処理)
        If String.IsNullOrEmpty(Me._Frm.lblSeikyuNoRelated.TextValue()) Then

            ' 対象データの検索
            Dim invDs As DataSet = New LMG000DS()
            Dim invDt As DataTable = invDs.Tables(LMGControlC.TABLE_NM_GET_INV_IN)
            Dim invDr As DataRow = invDt.NewRow()
            With Me._Frm
                invDr.Item("SEIQTO_CD") = .txtSeikyuCd.TextValue()
                invDr.Item("SKYU_DATE") = .imdInvDate.TextValue()
                invDr.Item("NRS_BR_CD") = .cmbBr.SelectedValue.ToString()
            End With
            invDt.Rows.Add(invDr)

            invDs = MyBase.CallWSA("LMG900BLF", "GetInvFrom", invDs)

            ' 取得した請求開始日の設定用加工
            Dim startDate As String = String.Empty
            Dim getDate As String = invDs.Tables(LMGControlC.TABLE_NM_GET_INV_OUT).Rows(0)("SKYU_DATE_FROM").ToString()
            If String.IsNullOrEmpty(getDate) Then
                startDate = "00000000"
            Else
                startDate = Date.ParseExact(getDate, "yyyyMMdd", Nothing).AddDays(1).ToString.Replace("/", "").Substring(0, 8)
            End If

            Dim rtnDt As DataTable = prm.ParamDataSet.Tables(LMG900C.TABLE_NM_DATE)
            Dim rtnDr As DataRow = rtnDt.NewRow()
            rtnDr.Item("SKYU_DATE_FROM") = startDate
            rtnDt.Rows.Add(rtnDr)

        End If

        Dim setDt As DataTable = Me._FindDs.Tables(LMG050C.TABLE_NM_DTL)
        Dim importCnt As Integer = rtnDs.Tables(LMG050C.TABLE_NM_DTL).Rows.Count
        Dim max As Integer

        Dim setDr As DataRow

        ' 最大枝番の最新化
        Me._Frm.lblMaxEdaban.TextValue = (Convert.ToInt32(Me._Frm.lblMaxEdaban.TextValue) + importCnt).ToString()

        ' レコードNoの設定
        Dim recNo As Integer = setDt.Rows.Count

        ' 取込データの格納
        For Each dr As DataRow In rtnDs.Tables(LMG050C.TABLE_NM_DTL).Rows
            If Convert.ToDecimal(dr.Item("KEISAN_TLGK")) = 0D Then
                Continue For
            End If
            setDt.ImportRow(dr)
        Next

        ' 多通貨対応
        For j As Integer = 0 To setDt.Rows.Count - 1

            setDr = setDt.Rows(j)
            If ds.Tables("LMG050_CURRINFO").Rows.Count > 0 Then
                setDr("ROUND_POS") = Convert.ToInt32(ds.Tables("LMG050_CURRINFO").Rows(0).Item("ROUND_POS"))
                setDr("ITEM_CURR_CD") = ds.Tables("LMG050_CURRINFO").Rows(0).Item("ITEM_CURR_CD").ToString()
            Else
                setDr("ROUND_POS") = 0
                setDr("ITEM_CURR_CD") = "JPY"
            End If

        Next

        max = setDt.Rows.Count - 1
        For i As Integer = 0 To max
            If String.IsNullOrEmpty(setDt.Rows(i).Item("RECORD_NO").ToString()) Then
                setDt.Rows(i).Item("RECORD_NO") = recNo.ToString().PadLeft(2, Convert.ToChar("0"))
                recNo = recNo + 1
            End If
        Next

        Return prm.ParamDataSet

    End Function

    ''' <summary>
    ''' 初期化処理(印刷済時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InsertCopyKuro()

        Dim msg As String = "初期化処理"

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMG050C.EventShubetsu.INITIALIZE) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsCommonChk(msg) = False Then
            Call Me.EndAction()  '終了処理
            Exit Sub
        End If

        '処理続行確認
        If Me.ConfirmMsg(msg) = False Then
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        'DataSet設定
        Call Me.SetDataSetEventData(Me._FindDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "InsCopyKuro")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG050BLF", "InsCopyKuro", Me._FindDs)
        Dim rtnHedDr As DataRow = rtnDs.Tables(LMG050C.TABLE_NM_HED).Rows(0)

        'メッセージコードの判定
        If MyBase.IsMessageExist = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me.EndAction() '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "InsCopyKuro")

        '終了処理
        Call Me.EndAction()

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        '請求番号再設定
        Me._PrmDs.Tables(LMG050C.TABLE_NM_IN).Rows(0).Item("SKYU_NO") = rtnHedDr.Item("SKYU_NO")

        '再検索処理を行う
        Call Me.SelectData()

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._KeiriModoshi_Flg)

        'メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G002", New String() {msg, ""})

        'フォーカスの設定
        Call Me.SetFocus()

    End Sub

    ''' <summary>
    ''' 請求先マスタ照会
    ''' </summary>
    ''' <param name="actionType"></param>
    ''' <returns>【特殊処理】Falseで返った場合は画面強制終了時なので、その後の処理を回避すること</returns>
    Private Function SetReturnSeiqtoPop(ByVal actionType As LMG050C.EventShubetsu) As Boolean

        Dim ds As DataSet = New LMZ220DS()
        Dim dt As DataTable = ds.Tables(LMZ220C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMG050C.EventShubetsu.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("SEIQTO_CD") = Me._Frm.txtSeikyuCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'パラメータ設定 
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        LMFormNavigate.NextFormNavigate(Me, "LMZ220", prm)

        '******* 返却パラメータの設定 *******
        If prm.ReturnFlg = True Then

            Dim rtnDr As DataRow = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)

            '区分マスタ または 名義銀行Mより検索
            Dim meigiNm As String = String.Empty
            meigiNm = Me._G.GetKouzaMeigiNm(Me._Frm.imdInvDate.TextValue, rtnDr.Item("KOUZA_KB").ToString())

            '戻り値の設定
            Me._Frm.txtSeikyuCd.TextValue = rtnDr.Item("SEIQTO_CD").ToString()
            Me._Frm.lblSeikyuNm.TextValue = rtnDr.Item("SEIQTO_NM").ToString()
            Me._Frm.txtSeikyuTantoNm.TextValue = rtnDr.Item("OYA_PIC").ToString()
            Me._Frm.lblSikyuMeigi.TextValue = meigiNm
            Me._ControlG.SetCheckBox(Me._Frm.chkMainAri, rtnDr.Item("DOC_SEI_YN").ToString())
            Me._ControlG.SetCheckBox(Me._Frm.chkSubAri, rtnDr.Item("DOC_HUKU_YN").ToString())
            Me._ControlG.SetCheckBox(Me._Frm.chkKeiHikaeAri, rtnDr.Item("DOC_HIKAE_YN").ToString())
            Me._ControlG.SetCheckBox(Me._Frm.chkHikaeAri, rtnDr.Item("DOC_KEIRI_YN").ToString())
            Dim rate As Decimal = Me._ControlH.ToHalfAdjust(Convert.ToDecimal(rtnDr.Item("TOTAL_NR")), 2)
            Dim nebikiKei As Decimal = Me._ControlH.ToHalfAdjust(Convert.ToDecimal(rtnDr.Item("TOTAL_NG")), 0)
            Me._Frm.numNebikiRateK.Value = rate.ToString()
            '2011/08/28 須賀 削除 スタート
            'Me._Frm.numNebikiRateM.Value = rate.ToString()
            '2011/08/28 須賀 削除 エンド
            Me._Frm.numNebikiGakuK.Value = nebikiKei.ToString()
            '2011/08/28 須賀 削除 スタート
            'Me._Frm.numNebikiGakuM.Value = nebikiKei.ToString()
            '2011/08/28 須賀 削除 エンド

            ' 請求先判定(TSMCか否か)
            Me._IsTsmc = Me.IsTsmc()
            ' TSMCか否かによる 取込項目の表示制御と初期値制御
            Me._G.SetControlsStatus2(Me._IsTsmc)

            '請求通貨CD設定
            If Not Me.setCurrList() Then
                '強制終了（終了確認を回避するために参照モードに変更）
                Me._Frm.lblSituation.DispMode = DispMode.VIEW
                Me._Frm.Close()
                Return False
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' スプレッド用照会
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="objNm"></param>
    ''' <param name="EventShubetsu"></param>
    Private Function SetReturnSpreadPop(ByVal frm As LMG050F, ByVal objNm As String, ByVal EventShubetsu As LMG050C.EventShubetsu) As Boolean

        Dim spr As Win.Spread.LMSpread = frm.sprSeikyuM

        With spr.ActiveSheet

            '行がなければ抜ける
            If .Rows.Count <= 0 Then
                Return True
            End If

            Dim cell As FarPoint.Win.Spread.Cell = .ActiveCell
            Dim colNo As Integer = cell.Column.Index
            Dim rowNo As Integer = cell.Row.Index

            Select Case colNo
                Case LMG050G.sprSeikyuMDef.TCUST_BPCD.ColNo
                    '真荷主コード

                    'セルがロック状態なら抜ける
                    If (cell.Locked) OrElse (.Columns(cell.Column.Index).Locked) Then
                        Return True
                    End If

                    '処理開始アクション
                    Select Case EventShubetsu
                        Case LMG050C.EventShubetsu.ENTER, LMG050C.EventShubetsu.SAVE
                            '何もしない

                        Case Else
                            Call Me.StartAction()
                    End Select

                    '真荷主参照POP起動
                    Call Me.ShowTcustPopup(frm, rowNo, EventShubetsu)
            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 真荷主参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rowNo"></param>
    ''' <param name="EventShubetsu"></param>
    ''' <returns></returns>
    Private Function ShowTcustPopup(ByVal frm As LMG050F, ByVal rowNo As Integer, ByVal EventShubetsu As LMG050C.EventShubetsu) As Boolean

        Dim ds As DataSet = New LMZ350DS()
        Dim dt As DataTable = ds.Tables(LMZ350C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            Select Case EventShubetsu
                Case LMG050C.EventShubetsu.ENTER, LMG050C.EventShubetsu.SAVE
                    .Item("BP_CD") = Me._ControlV.GetCellValue(frm.sprSeikyuM.ActiveSheet.Cells(rowNo, LMG050G.sprSeikyuMDef.TCUST_BPCD.ColNo))
                    .Item("CNT_CHK_ONLY_FLG") = LMConst.FLG.ON

                Case Else
                    .Item("CNT_CHK_ONLY_FLG") = LMConst.FLG.OFF
            End Select
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'パラメータ設定 
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        LMFormNavigate.NextFormNavigate(Me, "LMZ350", prm)

        '真荷主Popの戻り値を設定
        If prm.ReturnFlg = True Then
            If prm.ParamDataSet.Tables(LMZ350C.TABLE_NM_OUT).Rows.Count = 0 Then
                frm.sprSeikyuM.SetCellValue(rowNo, LMG050G.sprSeikyuMDef.TCUST_BPNM.ColNo, String.Empty)
                Return False

            Else
                Dim drPrm As DataRow = prm.ParamDataSet.Tables(LMZ350C.TABLE_NM_OUT).Rows(0)

                With Me._Frm
                    frm.sprSeikyuM.SetCellValue(rowNo, LMG050G.sprSeikyuMDef.TCUST_BPCD.ColNo, drPrm.Item("BP_CD").ToString())
                    frm.sprSeikyuM.SetCellValue(rowNo, LMG050G.sprSeikyuMDef.TCUST_BPNM.ColNo, drPrm.Item("BP_NM1").ToString())
                End With

                Return True
            End If
        End If

    End Function

    ''' <summary>
    ''' 請求項目マスタ照会
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetReturnSeiqKmkPop(ByVal dsDef As DataSet)

        Dim ds As DataSet = New LMZ190DS()
        Dim dt As DataTable = ds.Tables(LMZ190C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            '2014.09.12 追加START 多通貨対応
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue
            '2014.09.12 追加END　多通貨対応
        End With
        dt.Rows.Add(dr)

        'パラメータ設定 
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = ds

        'Pop起動
        LMFormNavigate.NextFormNavigate(Me, "LMZ190", prm)

        '******* 返却パラメータの設定 *******
        If prm.ReturnFlg = True Then

            Dim setDt As DataTable = Me._FindDs.Tables(LMG050C.TABLE_NM_DTL)
            Dim recNo As String = setDt.Rows.Count.ToString()
            Dim edaNo As Integer = Convert.ToInt32(Me._Frm.lblMaxEdaban.TextValue) + 1

            '最大枝番を最新にする
            Me._Frm.lblMaxEdaban.TextValue = edaNo.ToString()

            Dim setDr As DataRow = setDt.NewRow()

            With setDr

                '明細初期値を設定
                .Item("RECORD_NO") = recNo.ToString().PadLeft(2, Convert.ToChar("0"))
                .Item("SKYU_NO") = Me._Frm.lblSeikyuNo.TextValue
                .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue
                .Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                .Item("INS_FLG") = LMConst.FLG.ON
                .Item("PRINT_SORT") = "99"
                .Item("TEMPLATE_IMP_FLG") = LMG050C.MITORIKOMI
                .Item("SKYU_SUB_NO") = String.Empty '保存時に再設定するため
                .Item("SKYU_DATE_FROM") = String.Empty
                .Item("MAKE_SYU_KB") = LMGControlC.DETAIL_SAKUSEI_ADD
                .Item("MAKE_SYU_KB_NM") = Me._ControlV.SelectKbnData(LMGControlC.DETAIL_SAKUSEI_ADD, LMKbnConst.KBN_K021)
                .Item("BUSYO_CD") = Me.GetLoginUserBusyoCd()
                .Item("KANJO_KAMOKU_CD") = String.Empty
                .Item("KEIRI_BUMON_CD") = String.Empty
                .Item("KEISAN_TLGK") = 0
                .Item("NEBIKI_RT") = 0
                .Item("NEBIKI_GK") = 0
                .Item("TEKIYO") = String.Empty
                '2014.08.21 修正START 多通貨対応
                If setDt.Rows.Count > 0 Then
                    .Item("ITEM_CURR_CD") = setDt.Rows(0).Item("ITEM_CURR_CD").ToString()
                    .Item("ROUND_POS") = Convert.ToInt32(setDt.Rows(0).Item("ROUND_POS"))
                Else

                    Dim ds050 As DataSet = New LMG050DS()
                    Dim dt050 As DataTable = ds050.Tables(LMG050C.TABLE_NM_HED)
                    Dim dr050 As DataRow = dt050.NewRow()

                    With Me._Frm

                        dr050.Item("NRS_BR_CD") = .cmbBr.SelectedValue
                        dr050.Item("SEIQTO_CD") = .txtSeikyuCd.TextValue

                    End With

                    dt050.Rows.Add(dr050)
                    '==========================
                    'WSAクラス呼出
                    '==========================
                    ds050 = MyBase.CallWSA("LMG050BLF", "setCurrList", ds050)
                    If ds050.Tables("LMG050_CURRINFO").Rows.Count > 0 Then
                        .Item("ITEM_CURR_CD") = ds050.Tables("LMG050_CURRINFO").Rows(0).Item("ITEM_CURR_CD").ToString()
                        .Item("ROUND_POS") = Convert.ToInt32(ds050.Tables("LMG050_CURRINFO").Rows(0).Item("ROUND_POS"))
                    Else
                        .Item("ITEM_CURR_CD") = "JPY"
                        .Item("ROUND_POS") = 0
                    End If

                End If
                '2014.08.21 修正END   多通貨対応
                If dsDef.Tables("LMG050DEF_SEG").Rows.Count > 0 Then
                    .Item("PRODUCT_SEG_CD") = dsDef.Tables("LMG050DEF_SEG").Rows(0).Item("DEF_SEG_SEIHIN").ToString()
                    .Item("ORIG_SEG_CD") = dsDef.Tables("LMG050DEF_SEG").Rows(0).Item("DEF_SEG_CHIIKI").ToString()
                    .Item("DEST_SEG_CD") = dsDef.Tables("LMG050DEF_SEG").Rows(0).Item("DEF_SEG_CHIIKI").ToString()
                    .Item("TCUST_BPCD") = dsDef.Tables("LMG050DEF_SEG").Rows(0).Item("TCUST_BPCD").ToString()
                    .Item("TCUST_BPNM") = dsDef.Tables("LMG050DEF_SEG").Rows(0).Item("TCUST_BPNM").ToString()
                End If

                '戻り値を設定
                Dim rtnDr As DataRow = prm.ParamDataSet.Tables(LMZ190C.TABLE_NM_OUT).Rows(0)
                .Item("GROUP_KB") = rtnDr.Item("GROUP_KB")
                .Item("SEIQKMK_CD") = rtnDr.Item("SEIQKMK_CD")
                .Item("SEIQKMK_NM") = rtnDr.Item("SEIQKMK_NM")
                .Item("KEIRI_KB") = rtnDr.Item("KEIRI_KB")
                .Item("TAX_KB") = rtnDr.Item("TAX_KB")
                .Item("TAX_KB_NM") = Me._ControlV.SelectKbnData(rtnDr.Item("TAX_KB").ToString(), LMKbnConst.KBN_Z001)
                .Item("SEIQKMK_CD_S") = rtnDr.Item("SEIQKMK_CD_S")

            End With

            setDt.Rows.Add(setDr)

        End If

    End Sub

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <param name="msg">メッセージ置換文字列(処理名)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmMsg(ByVal msg As String) As Boolean

        If MyBase.ShowMessage(Me._Frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then

            'メッセージの設定
            Call Me.SetBaseMsg()

            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 基本メッセージ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetBaseMsg()

        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then

            '編集時設定用
            MyBase.ShowMessage(Me._Frm, "G003")

        ElseIf Me._Frm.lblSituation.DispMode.Equals(DispMode.VIEW) Then

            '参照時設定用
            MyBase.ShowMessage(Me._Frm, "G006")

        End If

    End Sub

    '★ UPD START 2011/09/06 SUGA
    ''' <summary>
    ''' 全項目計算/勘定科目コードの設定を行う
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetCalcAll(Optional ByVal calcFlg As Boolean = False) As Boolean

        '勘定科目コード設定
        Dim max As Integer = Me._Frm.sprSeikyuM.ActiveSheet.Rows.Count - 1
        For i As Integer = 0 To max
            Me._Frm.sprSeikyuM.SetCellValue(i, LMG050G.sprSeikyuMDef.KANJOKMK_CD.ColNo, Me.SetKanjoKmkCd(i))
        Next

        If calcFlg Then
            ' 率値引額計算対象時のみ処理する
            For i As Integer = 0 To max

                '率値引額
                If Me.RateNebiki(i) = False Then
                    Return False
                End If

            Next

            ' 計算用のDataTableを取得
            Dim calcDs As DataSet = Me._FindDs.Copy()
            Dim calcDtlTbl As DataTable = calcDs.Tables(LMG050C.TABLE_NM_DTL)
            calcDtlTbl.Clear()

            ' 計算用のDataTableにSpreadの情報を設定
            Call Me.SetDataSetSaveDtl(calcDs)

            ' 率値引額の端数調整
            Call Me.EditNebikiRitsuGaku(calcDs)

            ' 計算用のDataTableにSpreadの情報を設定
            Call Me._G.SetSpread(calcDtlTbl, _dsCombo)
        End If

        For i As Integer = 0 To max

            '請求額
            If Me.CalcTotal(i) = False Then
                Return False
            End If

        Next

        '集計部の計算を行う
        If Me.HedCalc(LMGControlC.TAX_KAZEI) = False Then
            Return False
        End If
        If Me.HedCalc(LMGControlC.TAX_MENZEI) = False Then
            Return False
        End If
        If Me.HedCalc(LMGControlC.TAX_HIKAZEI) = False Then
            Return False
        End If
        If Me.HedCalc(LMGControlC.TAX_UCHIZEI) = False Then
            Return False
        End If

        Return True

    End Function
    '★ UPD E N D 2011/09/06 SUGA

    ''' <summary>
    ''' 明細部計算処理
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="colNo">列番号</param>
    ''' <remarks></remarks>
    Private Function SprCalc(ByVal rowNo As Integer, ByVal colNo As Integer) As Boolean

        If colNo.Equals(LMG050G.sprSeikyuMDef.KEISAN_GAKU.ColNo) _
        OrElse colNo.Equals(LMG050G.sprSeikyuMDef.NEBIKI_RATE.ColNo) Then

            '率値引額
            If Me.RateNebiki(rowNo) = False Then
                Return False
            End If

        End If

        '請求額
        If Me.CalcTotal(rowNo) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 率値引額を求める
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Function RateNebiki(ByVal rowNo As Integer) As Boolean

        Dim rtnResult As Boolean = True

        With Me._Frm.sprSeikyuM.ActiveSheet

            '入力された値を取得する
            Dim keisanGk As String = Me._ControlV.GetCellValue(.Cells(rowNo, LMG050G.sprSeikyuMDef.KEISAN_GAKU.ColNo))
            Dim nebikiRate As String = Me._ControlV.GetCellValue(.Cells(rowNo, LMG050G.sprSeikyuMDef.NEBIKI_RATE.ColNo))

            '率値引額を求める
            Dim nebiki As Decimal = Convert.ToDecimal(keisanGk) * Convert.ToDecimal(nebikiRate) / 100

            '有効桁数を考慮する(小数点以下切捨て)
            nebiki = Math.Floor(nebiki)

            'オーバーフローチェックを行う
            '★ UPD START 2011/09/06 SUGA
            '2011/08/12 菱刈 オーバーフローチェックコメント化 スタート
            If Me._V.IsOverFlowChk(nebiki.ToString(), "率値引額") = False Then
                Me._ControlV.SetErrorControl(Me._Frm.sprSeikyuM, rowNo, LMG050G.sprSeikyuMDef.NEBIKI_RATE.ColNo)
                rtnResult = False
                nebiki = 0
            End If
            '2011/08/12 菱刈 オーバーフローチェックコメント化 エンド
            '★ UPD E N D 2011/09/06 SUGA
            '値を画面に設定
            Me._Frm.sprSeikyuM.SetCellValue(rowNo, LMG050G.sprSeikyuMDef.RATENEBIKI_GAKU.ColNo, nebiki.ToString())

        End With

        Return rtnResult

    End Function

    ''' <summary>
    ''' 請求額を求める
    ''' </summary>
    ''' <param name="rowNo">対象行番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CalcTotal(ByVal rowNo As Integer) As Boolean

        Dim rtnResult As Boolean = True

        With Me._Frm.sprSeikyuM.ActiveSheet

            '入力された値を取得する
            Dim keisanGk As String = Me._ControlV.GetCellValue(.Cells(rowNo, LMG050G.sprSeikyuMDef.KEISAN_GAKU.ColNo))
            Dim rateNebiki As String = Me._ControlV.GetCellValue(.Cells(rowNo, LMG050G.sprSeikyuMDef.RATENEBIKI_GAKU.ColNo))
            Dim koteiNebiki As String = Me._ControlV.GetCellValue(.Cells(rowNo, LMG050G.sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo))
            Dim total As Decimal = 0

            'マイナス値チェックを行う
            If rtnResult AndAlso Me._V.ChkInputMinus(koteiNebiki, "固定値引額", "999,999,999") = False Then
                rtnResult = False
                total = 0
                Me._ControlV.SetErrorControl(Me._Frm.sprSeikyuM, rowNo, LMG050G.sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo)

            End If

            If rtnResult Then
                '請求額を求める
                total = Convert.ToDecimal(keisanGk) - (Convert.ToDecimal(rateNebiki) + Convert.ToDecimal(koteiNebiki))
            End If

            '2011/08/12 菱刈 オーバーフローチェックコメント化 スタート
            'オーバーフローチェックを行う
            If rtnResult AndAlso Me._V.IsOverFlowChk(total.ToString(), "請求額") = False Then
                rtnResult = False
                total = 0
                Me._ControlV.SetErrorControl(Me._Frm.sprSeikyuM, rowNo, LMG050G.sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo)
            End If
            '2011/08/12 菱刈 オーバーフローチェックコメント化 エンド

            '値を画面に設定
            Me._Frm.sprSeikyuM.SetCellValue(rowNo, LMG050G.sprSeikyuMDef.SEIKYU_GAKU.ColNo, total.ToString())

        End With

        Return rtnResult

    End Function

    ''' <summary>
    ''' ヘッダ部計算処理
    ''' </summary>
    ''' <param name="tax"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function HedCalc(ByVal tax As String) As Boolean

        Dim rtnResult As Boolean = True

        Dim filter08 As String = String.Empty
        Dim motoTax As String = String.Empty

        With Me._Frm

            '計算処理を行ったとみなす
            _KeisanFlg = True

            '①計算総額を求める
            Dim max As Integer = .sprSeikyuM.ActiveSheet.Rows.Count - 1
            Dim total As Decimal = 0
            For i As Integer = 0 To max

                '2014.09.10 多通貨対応　修正START
                filter08 = String.Empty
                filter08 = String.Concat(filter08, "SYS_DEL_FLG = '0'")
                filter08 = String.Concat(filter08, " AND KBN_GROUP_CD = '", LMKbnConst.KBN_Z001, "' ")
                filter08 = String.Concat(filter08, " AND KBN_CD = '", Me._ControlV.GetCellValue(.sprSeikyuM.ActiveSheet.Cells(i, LMG050G.sprSeikyuMDef.KAZEI_KBN.ColNo)), "' ")

                Dim kbnNm08Dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter08)

                If tax.Equals(Me._ControlV.GetCellValue(.sprSeikyuM.ActiveSheet.Cells(i, LMG050G.sprSeikyuMDef.KAZEI_KBN.ColNo))) Then
                    total = total + Convert.ToDecimal(Me._ControlV.GetCellValue(.sprSeikyuM.ActiveSheet.Cells(i, LMG050G.sprSeikyuMDef.SEIKYU_GAKU.ColNo)))
                    If kbnNm08Dr.Length > 0 Then
                        tax = kbnNm08Dr(0)("KBN_NM8").ToString()
                        motoTax = Me._ControlV.GetCellValue(.sprSeikyuM.ActiveSheet.Cells(i, LMG050G.sprSeikyuMDef.KAZEI_KBN.ColNo))
                    End If
                Else

                    If kbnNm08Dr.Length > 0 AndAlso tax.Equals(kbnNm08Dr(0)("KBN_NM8").ToString()) = True Then
                        total = total + Convert.ToDecimal(Me._ControlV.GetCellValue(.sprSeikyuM.ActiveSheet.Cells(i, LMG050G.sprSeikyuMDef.SEIKYU_GAKU.ColNo)))
                        tax = kbnNm08Dr(0)("KBN_NM8").ToString()
                        motoTax = Me._ControlV.GetCellValue(.sprSeikyuM.ActiveSheet.Cells(i, LMG050G.sprSeikyuMDef.KAZEI_KBN.ColNo))
                    End If

                End If
                '2014.09.10 多通貨対応　修正END

            Next

            '②全体値引率
            '④全体値引額取得
            Dim rate As Decimal = 0
            Dim rateNebiki As Decimal = 0
            Dim nebikiGk As Decimal = 0
            Select Case tax
                Case LMGControlC.TAX_KAZEI _
                    , LMGControlC.TAX_MENZEI
                    If tax.Equals(LMGControlC.TAX_KAZEI) Then
                        rate = Convert.ToDecimal(.numNebikiRateK.Value)
                        nebikiGk = Convert.ToDecimal(.numNebikiGakuK.Value)
                    Else
                        rate = Convert.ToDecimal(.numNebikiRateM.Value)
                        nebikiGk = Convert.ToDecimal(.numNebikiGakuM.Value)
                    End If

                    '③全体値引率による値引額を求める(小数点以下切捨て)
                    rateNebiki = Math.Floor(total * rate / 100)

            End Select

            '⑤税額を求める
            Dim taxGk As Decimal = 0
            Dim taxRate As Decimal = 0
            Dim hasu As Decimal = 0
            Select Case tax
                Case LMGControlC.TAX_KAZEI _
                    , LMGControlC.TAX_UCHIZEI

                    Dim filter As String = String.Empty
                    filter = String.Concat(filter, "SYS_DEL_FLG = '0'")
                    filter = String.Concat(filter, " AND KBN_GROUP_CD = '", LMKbnConst.KBN_Z001, "' ")
                    '2014.09.10 多通貨対応　修正START
                    If String.IsNullOrEmpty(motoTax) = True Then
                        filter = String.Concat(filter, " AND KBN_CD = '", tax, "' ")
                    Else
                        filter = String.Concat(filter, " AND KBN_CD = '", motoTax, "' ")
                    End If
                    '2014.09.10 多通貨対応　修正END

                    '区分マスタより、税コードを取得
                    Dim kbnDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
                    If kbnDr.Count > 0 Then
                        Dim taxCd As String = kbnDr(0).Item("KBN_NM3").ToString
                        '税マスタより、税率を取得
                        filter = String.Empty
                        filter = String.Concat(filter, "SYS_DEL_FLG = '0'")
                        filter = String.Concat(filter, " AND TAX_CD = '", taxCd, "' ")
                        '2011/08/12 菱刈 残作業No15 スタート
                        Dim Start_Data As String = .imdInvDate.TextValue
                        '請求日に値が入力されていなかった場合
                        If String.IsNullOrEmpty(Start_Data) = True Then

                            filter = String.Concat(filter, " AND START_DATE <= '", MyBase.GetSystemDateTime(0), "' ")
                        Else
                            '請求日に値が入力されている場合
                            filter = String.Concat(filter, " AND START_DATE <= '", Start_Data, "' ")
                        End If

                        '2011/08/12 菱刈 残作業No15 エンド

                        Dim taxDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TAX).Select(filter, "START_DATE desc")
                        If taxDr.Count > 0 Then
                            taxRate = Convert.ToDecimal(taxDr(0).Item("TAX_RATE").ToString)
                        End If

                    End If

                    If tax.Equals(LMGControlC.TAX_KAZEI) Then
                        taxGk = Me._ControlH.ToHalfAdjust((total - (rateNebiki + nebikiGk)) * taxRate, 0)

                        '⑥税端数処理取得
                        hasu = Convert.ToDecimal(.numZeiHasuK.Value)
                    Else
                        '内税：小数点以下切捨て
                        'taxGk = Math.Floor(total * taxRate / (taxRate + 1))
                        taxGk = Math.Truncate(total * taxRate / (taxRate + 1))
                        total = total - taxGk
                    End If

            End Select

            '⑦請求額を求める
            Dim skyuGk As Decimal = 0
            Select Case tax
                Case LMGControlC.TAX_KAZEI _
                , LMGControlC.TAX_MENZEI
                    skyuGk = total - (rateNebiki + nebikiGk) + taxGk + hasu
                Case LMGControlC.TAX_UCHIZEI
                    skyuGk = taxGk + total
                Case Else
                    skyuGk = total
            End Select

            'オーバーフローチェックを行う
            If Me.OverFlowChk(tax, total, rateNebiki, taxGk, skyuGk) = False Then
                rtnResult = False
            End If

            '計算結果を各コントロールに設定する
            Call Me._G.SetHedCalcData(tax _
                                      , total.ToString() _
                                      , rateNebiki.ToString() _
                                      , taxGk.ToString() _
                                      , skyuGk.ToString()
                                      )

            Dim allTotal As Decimal = Convert.ToDecimal(.numSeikyuGakuK.Value) _
                                    + Convert.ToDecimal(.numSeikyuGakuM.Value) _
                                    + Convert.ToDecimal(.numSeikyuGakuH.Value) _
                                    + Convert.ToDecimal(.numSeikyuGakuU.Value)

            '請求額のオーバーフローチェックを行う
            ' Dim max10 As Decimal = 9999999999
            '2011/08/12 菱刈 オーバーフローチェックコメント化 スタート
            'If Me._V.IsOverFlowChk(allTotal.ToString(), "請求書総額") = False Then
            '    allTotal = max10
            '    rtnResult = False
            'End If
            '2011/08/12 菱刈 オーバーフローチェックコメント化 エンド
            .numSeikyuAll.Value = allTotal.ToString()

            Return rtnResult

        End With

    End Function
    '★ ADD START 2011/09/06 SUGA
    ''' <summary>
    ''' 率値引額を端数調整
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function EditNebikiRitsuGaku(ByVal ds As DataSet) As DataSet

        'DBデータ取得データセット設定
        Dim setTbl As DataTable = ds.Tables(LMG050C.TABLE_NM_DTL)
        Dim setDrCount As Integer = setTbl.Rows.Count
        Dim setDr As DataRow = Nothing

        If (setTbl Is Nothing) _
           OrElse setTbl.Rows.Count = 0 Then
            ' DBより取得データが存在しない場合は処理を抜ける
            Return ds
        End If

        Dim max As Integer = setTbl.Rows.Count - 1

        '出力データ設定
        Dim rtnDs As DataSet = ds.Copy()
        Dim rtnSetTbl As DataTable = rtnDs.Tables(LMG050C.TABLE_NM_DTL)
        rtnSetTbl.Clear()

        Dim filter As String = String.Empty
        filter = "MAKE_SYU_KB <> '00' OR SYS_DEL_FLG <> '1'"
        ' 手動データのみ抽出
        Dim selectShudoDr As DataRow() = setTbl.Select(filter)

        For i As Integer = 0 To selectShudoDr.Length - 1
            ' 手動データのみ設定
            rtnSetTbl.ImportRow(selectShudoDr(i))
        Next

        filter = String.Empty
        filter = "MAKE_SYU_KB = '00' AND SYS_DEL_FLG = '0'"
        Dim sort As String = "GROUP_KB, SEIQKMK_CD, TAX_KB, MAKE_SYU_KB, NEBIKI_RT, SKYU_SUB_NO"
        ' 自動取込データのみ抽出
        Dim selectAutoDr As DataRow() = setTbl.Select(filter, sort)
        Dim selectAutoDrCnt As Integer = selectAutoDr.Length - 1

        '変数宣言
        ' 集計対象金額項目
        Dim sumKeisanTlGk As Decimal = 0        ' 計算金額（値引前請求額）
        Dim sumRitsuNebikiGk As Decimal = 0     ' 率値引額（算出した値）
        ' 計算用金額項目
        Dim resRitsuNebikiGk As Decimal = 0     ' 率値引額（キーブレーク項目毎に集計した計算額に対する率値引額）
        ' キーブレーク用項目
        Dim keyGroupCd As String = String.Empty
        Dim keySeiqKmkCd As String = String.Empty
        Dim keyTaxKb As String = String.Empty
        Dim keyMakeSyuKb As String = String.Empty
        Dim keyNebikiRt As Decimal = 0

        If selectAutoDrCnt >= 0 Then
            ' 1行以上存在する場合
            keyGroupCd = selectAutoDr(0).Item("GROUP_KB").ToString
            keySeiqKmkCd = selectAutoDr(0).Item("SEIQKMK_CD").ToString
            keyTaxKb = selectAutoDr(0).Item("TAX_KB").ToString
            keyMakeSyuKb = selectAutoDr(0).Item("MAKE_SYU_KB").ToString
            keyNebikiRt = Convert.ToDecimal(selectAutoDr(0).Item("NEBIKI_RT"))
        End If

        For i As Integer = 0 To selectAutoDrCnt
            With selectAutoDr(i)
                If keyGroupCd.Equals(.Item("GROUP_KB")) _
                    AndAlso keySeiqKmkCd.Equals(.Item("SEIQKMK_CD")) _
                    AndAlso keyTaxKb.Equals(.Item("TAX_KB")) _
                    AndAlso keyMakeSyuKb.Equals(.Item("MAKE_SYU_KB")) _
                    AndAlso keyNebikiRt = Convert.ToDecimal(.Item("NEBIKI_RT")) Then
                    ' 集計対象金額を加算
                    sumKeisanTlGk = sumKeisanTlGk + Convert.ToDecimal(.Item("KEISAN_TLGK"))
                    sumRitsuNebikiGk = sumRitsuNebikiGk + Convert.ToDecimal(.Item("NEBIKI_RTGK"))
                Else
                    ' ●キーブレーク項目で集計した計算額と値引率で、本来の率値引額を算出
                    resRitsuNebikiGk = Math.Floor(sumKeisanTlGk * keyNebikiRt / 100)

                    ' ●率値引額の端数調整を行う
                    selectAutoDr(i - 1).Item("NEBIKI_RTGK") = Convert.ToDecimal(selectAutoDr(i - 1).Item("NEBIKI_RTGK")) + resRitsuNebikiGk - sumRitsuNebikiGk

                    ' 集計対象金額を初期化して再設定
                    sumKeisanTlGk = 0
                    sumRitsuNebikiGk = 0
                    sumKeisanTlGk = Convert.ToDecimal(.Item("KEISAN_TLGK"))
                    sumRitsuNebikiGk = Convert.ToDecimal(.Item("NEBIKI_RTGK"))
                End If

                'キー項目を再設定
                keyGroupCd = .Item("GROUP_KB").ToString
                keySeiqKmkCd = .Item("SEIQKMK_CD").ToString
                keyTaxKb = .Item("TAX_KB").ToString
                keyMakeSyuKb = .Item("MAKE_SYU_KB").ToString
                keyNebikiRt = Convert.ToDecimal(.Item("NEBIKI_RT"))

            End With
        Next

        If selectAutoDrCnt >= 0 Then
            ' 1行以上存在する場合最終行を編集
            ' ●キーブレーク項目で集計した計算額と値引率で、本来の率値引額を算出
            resRitsuNebikiGk = Math.Floor(sumKeisanTlGk * keyNebikiRt / 100)
            ' ●率値引額の端数調整を行う
            selectAutoDr(selectAutoDrCnt).Item("NEBIKI_RTGK") = Convert.ToDecimal(selectAutoDr(selectAutoDrCnt).Item("NEBIKI_RTGK")) + resRitsuNebikiGk - sumRitsuNebikiGk
        End If

        For i As Integer = 0 To selectAutoDrCnt
            rtnSetTbl.ImportRow(selectAutoDr(i))
        Next

        Return rtnDs

    End Function
    '★ ADD E N D 2011/09/06 SUGA

    ''' <summary>
    ''' オーバーフローチェックを呼ぶ
    ''' </summary>
    ''' <param name="tax"></param>
    ''' <param name="total"></param>
    ''' <param name="RateNebiki"></param>
    ''' <param name="taxGk"></param>
    ''' <param name="skyuGk"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OverFlowChk(ByVal tax As String _
                                 , ByRef total As Decimal _
                                 , ByRef RateNebiki As Decimal _
                                 , ByRef taxGk As Decimal _
                                 , ByRef skyuGk As Decimal) As Boolean

        Dim rtnResult As Boolean = True

        With Me._Frm.sprSeikyuM.ActiveSheet

            '税区分ごとに設定するコントロールを取得
            Dim ctlTotal As Control = Nothing
            Dim ctlRateNebiki As Control = Nothing
            Dim ctlTaxGk As Control = Nothing
            Dim ctlSkyu As Control = Nothing

            Select Case tax
                Case LMGControlC.TAX_KAZEI
                    ctlTotal = Me._Frm.numCalAllK
                    ctlRateNebiki = Me._Frm.numRateNebikigakuK
                    ctlTaxGk = Me._Frm.numZeigakuK
                    ctlSkyu = Me._Frm.numSeikyuGakuK

                Case LMGControlC.TAX_MENZEI
                    ctlTotal = Me._Frm.numCalAllM
                    ctlRateNebiki = Me._Frm.numRateNebikigakuM
                    ctlSkyu = Me._Frm.numSeikyuGakuM

                Case LMGControlC.TAX_HIKAZEI
                    ctlTotal = Me._Frm.numCalAllH
                    ctlSkyu = Me._Frm.numSeikyuGakuH

                Case LMGControlC.TAX_UCHIZEI
                    ctlTotal = Me._Frm.numCalAllU
                    ctlTaxGk = Me._Frm.numZeigakuU
                    ctlSkyu = Me._Frm.numSeikyuGakuU
            End Select

            'オーバーフローチェックを行う
            '2011/08/12 菱刈 オーバーフローチェックコメント化 スタート
            'Dim max10 As Decimal = 9999999999
            'If Me._V.IsOverFlowChk(total.ToString(), "計算総額") = False Then
            '    total = max10
            '    rtnResult = False
            'End If

            'If Me._V.IsOverFlowChk(RateNebiki.ToString(), "全体値引率による値引額") = False Then
            '    RateNebiki = max10
            '    rtnResult = False
            'End If

            'If Me._V.IsOverFlowChk(taxGk.ToString(), "税額") = False Then
            '    taxGk = max10
            '    rtnResult = False
            'End If

            'If Me._V.IsOverFlowChk(skyuGk.ToString(), "請求額") = False Then
            '    skyuGk = max10
            '    rtnResult = False
            'End If
            '2011/08/12 菱刈 オーバーフローチェックコメント化 エンド
            Return rtnResult

        End With
    End Function

    ''' <summary>
    ''' 勘定科目コード設定処理
    ''' </summary>
    ''' <param name="rowNo">対象行番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetKanjoKmkCd(ByVal rowNo As Integer) As String

        '明細部の値を取得する
        Dim busyoCd As String = Me._ControlV.GetCellValue(Me._Frm.sprSeikyuM.ActiveSheet.Cells(rowNo, LMG050G.sprSeikyuMDef.BUSYO.ColNo))
        Dim keiriKb As String = Me._ControlV.GetCellValue(Me._Frm.sprSeikyuM.ActiveSheet.Cells(rowNo, LMG050G.sprSeikyuMDef.KEIRI_KBN.ColNo))
        'ADD 2016/09/06
        Dim JisyaTasyaKB As String = Me._ControlV.GetCellValue(Me._Frm.sprSeikyuM.ActiveSheet.Cells(rowNo, LMG050G.sprSeikyuMDef.JISYATASYA_KB.ColNo))

        Return Me._ControlH.SetKanjoKmkCd(busyoCd, keiriKb, JisyaTasyaKB)

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function PrintKagami() As Boolean

        Dim ds As DataSet = Nothing
        Dim dt As DataTable = Nothing
        Dim dr As DataRow = Nothing
        Dim id As String = String.Empty
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        Dim Printid As String = String.Empty
        '印刷種別によってデータセット変更

        With Me._Frm

            Dim Print As String = String.Empty
            Print = .cmbPrint.SelectedValue.ToString

            Select Case Print

                Case LMG050C.PRINT_SEIKYU_SHO, LMG050C.PRINT_IKKATSU

                    '請求書
                    id = "LMG520IN"
                    ds = New LMG520DS
                    dt = ds.Tables(id)
                    dr = dt.NewRow
                    Printid = "PrintSeiqto"

                    'データセット
                    dr("SKYU_NO") = .lblSeikyuNo.TextValue
                    dr("SEI") = .chkMainAri.GetBinaryValue()
                    dr("HUKU") = .chkSubAri.GetBinaryValue()
                    dr("KEIRIHIKAE") = .chkKeiHikaeAri.GetBinaryValue()
                    dr("HIKAE") = .chkHikaeAri.GetBinaryValue()
                    dr("NRS_BR_CD") = .cmbBr.SelectedValue.ToString
                    dr("SKYU_DATE") = .imdInvDate.TextValue

                    'START YANAI 要望番号661
                    '請求鑑出力レイアウト判定
                    Dim kagamiPtnFlg As Boolean = False
                    If Convert.ToDecimal(.numRateNebikigakuK.Value) > 0 OrElse _
                        Convert.ToDecimal(.numRateNebikigakuM.Value) > 0 OrElse _
                        Convert.ToDecimal(.numNebikiGakuK.Value) > 0 OrElse _
                        Convert.ToDecimal(.numNebikiGakuM.Value) > 0 Then
                        'ヘッダー部の「全体値引率による値引額（a * b） ： c」または「全体値引額 ： d」が0より大きい場合
                        kagamiPtnFlg = True
                    End If

                    With Me._Frm.sprSeikyuM.ActiveSheet

                        If kagamiPtnFlg = False Then
                            Dim max As Integer = .Rows.Count - 1
                            For i As Integer = 0 To max
                                If Convert.ToDecimal(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.RATENEBIKI_GAKU.ColNo))) > 0 OrElse _
                                    Convert.ToDecimal(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo))) > 0 Then
                                    '明細部の「率値引き額」または「固定値引き額」が0より大きい場合
                                    kagamiPtnFlg = True
                                    Exit For
                                End If
                            Next
                        End If
                    End With

                    If kagamiPtnFlg = False Then
                        dr("KAGAMI_PTN") = "00"
                    Else
                        dr("KAGAMI_PTN") = "01"
                    End If
                    'END YANAI 要望番号661

                    dr("PRINT_SYUBETSU") = Print
                    If LMG050C.PRINT_IKKATSU.Equals(Print) Then
                        dr("SEIQTO_CD") = .txtSeikyuCd.TextValue
                        dr("SEIQ_DATE_FROM") = Me.GetStartDate(.cmbBr.SelectedValue.ToString, .txtSeikyuCd.TextValue, .imdInvDate.TextValue)
                        dr("SEIQ_DATE_TO") = .imdInvDate.TextValue
                        dr("PRINT_LMG500_FLG") = Me.GetMeisaiPrintFlg(LMG050C.IkkatsuPrintMeisaiKbn.HokanNiyaku)
                        dr("PRINT_LMF510_FLG") = Me.GetMeisaiPrintFlg(LMG050C.IkkatsuPrintMeisaiKbn.Unchin)
                        dr("PRINT_LME500_FLG") = Me.GetMeisaiPrintFlg(LMG050C.IkkatsuPrintMeisaiKbn.Sagyo)
                        '請求先コードでの運賃締め基準の取得
                        dr("UNTIN_CALCULATION_KB") = Me.SimeKijun(.cmbBr.SelectedValue.ToString, .txtSeikyuCd.TextValue)

                    End If

                    ' 請求書出力内容変更 適用年月 (変更後帳票定義ファイル 使用開始年月) 設定
                    dr("RPT_CHG_START_YM") = Me._G.GetRptChgStartYm()

                Case LMG050C.PRINT_CHECK_LIST

                    'チェックリスト
                    id = "LMG530IN"
                    ds = New LMG530DS
                    dt = ds.Tables(id)
                    dr = dt.NewRow
                    Printid = "PrintCheck"


                    'データセット
                    dr("NRS_BR_CD") = .cmbBr.SelectedValue.ToString
                    dr("SKYU_NO") = .lblSeikyuNo.TextValue

            End Select

            'データセットの追加
            ds.Tables(id).Rows.Add(dr)
            ds.Merge(New RdPrevInfoDS)


            Dim rtnDs As DataSet = MyBase.CallWSA(blf, Printid, ds)

            'メッセージ判定
            If IsMessageExist() = True Then

                'エラーメッセージ判定
                If MyBase.IsErrorMessageExist() = False Then

                    If Print.Equals(LMG050C.PRINT_IKKATSU) = False OrElse Not (Not rtnDs.Tables(LMConst.RD) Is Nothing AndAlso rtnDs.Tables(LMConst.RD).Rows.Count <> 0) Then

                        '処理終了アクション
                        Call Me.EndAction()
                        '2011/08/02 菱刈 メッセージの変更 スタート
                        '印刷処理でエラーメッセージあったらメッセージを表示してG003を設定
                        ' MyBase.ShowMessage(Me._Frm)
                        MyBase.ShowMessage(Me._Frm, "S001", New String() {"印刷"})
                        '2011/08/02 菱刈 メッセージの変更 エンド
                        Return False

                    Else
                        MyBase.ShowMessage(Me._Frm, "G054")

                    End If

                End If
            End If

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


            '処理終了アクション
            Call Me.EndAction()

            MyBase.ShowMessage(Me._Frm)

            Return True

        End With

    End Function

    ''' <summary>
    ''' 各イベントで使用するデータセットの設定
    ''' </summary>
    ''' <param name="ds">格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetEventData(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMG050C.TABLE_NM_HED)
        Dim dr As DataRow = Nothing

        If dt.Rows.Count = 0 Then
            dr = dt.NewRow()
        Else
            dr = dt.Rows(0)
        End If

        With Me._Frm

            '共通項目
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            'Dim SeqCd As ArrayList = Me._ControlG.GetSeqNm(Me._Frm.txtSeikyuCd.TextValue)
            '20160927 要番2622 tsunehira add Start
            Dim SeqCd As ArrayList = Me._ControlG.GetSeqNm(Me._Frm.cmbBr.SelectedValue.ToString, Me._Frm.txtSeikyuCd.TextValue)
            '20160927 要番2622 tsunehira add End

            If SeqCd.Count >= 1 Then
                dr.Item("SEIQTO_CD") = SeqCd(1).ToString()
            End If
            dr.Item("SKYU_NO") = .lblSeikyuNo.TextValue
            dr.Item("SYS_UPD_DATE") = .lblSysUpdDate.TextValue
            dr.Item("SYS_UPD_TIME") = .lblSysUpdTime.TextValue

        End With

        If dt.Rows.Count = 0 Then
            dt.Rows.Add(dr)
        End If

    End Sub

    ''' <summary>
    ''' 入力内容を全てDataSetに格納する
    ''' </summary>
    ''' <param name="ds">更新内容を格納するデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetSave(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMG050C.TABLE_NM_HED)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm

            dr.Item("SKYU_NO") = .lblSeikyuNo.TextValue
            dr.Item("STATE_KB") = .cmbStateKbn.SelectedValue
            dr.Item("SKYU_DATE") = .imdInvDate.TextValue
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            'Dim SeqCd As ArrayList = Me._ControlG.GetSeqNm(Me._Frm.txtSeikyuCd.TextValue)
            '20160927 要番2622 tsunehira add Start
            Dim SeqCd As ArrayList = Me._ControlG.GetSeqNm(Me._Frm.cmbBr.SelectedValue.ToString, Me._Frm.txtSeikyuCd.TextValue)
            '20160927 要番2622 tsunehira add End

            If SeqCd.Count >= 1 Then
                dr.Item("SEIQTO_CD") = SeqCd(1).ToString()

            End If
            dr.Item("SEIQTO_PIC") = .txtSeikyuTantoNm.TextValue
            '2011/08/04 菱刈 請求先名称の取得をキャッシュから取得 スタート
            '請求先名称がブランクだったらキャッシュから取得
            '2013/04/04 kobayashi 要望管理1998 常にキャッシュから再取得
            If String.IsNullOrEmpty(Me._Frm.lblSeikyuNo.TextValue) Then
                If SeqCd.Count >= 1 Then
                    dr.Item("SEIQTO_NM") = SeqCd(0).ToString()
                Else
                    .lblSeikyuNm.TextValue = String.Empty
                    dr.Item("SEIQTO_NM") = .lblSeikyuNm.TextValue
                End If
            Else
                dr.Item("SEIQTO_NM") = .lblSeikyuNm.TextValue
            End If
            '2011/08/04 菱刈 請求先名称の取得をキャッシュから取得 エンド
            dr.Item("NEBIKI_RT1") = .numNebikiRateK.Value
            dr.Item("NEBIKI_GK1") = .numNebikiGakuK.Value
            dr.Item("TAX_GK1") = .numZeigakuK.Value
            dr.Item("TAX_HASU_GK1") = .numZeiHasuK.Value
            dr.Item("NEBIKI_RT2") = .numNebikiRateM.Value
            dr.Item("NEBIKI_GK2") = .numNebikiGakuM.Value
            dr.Item("CRT_KB") = .cmbSeiqtShubetu.SelectedValue
            dr.Item("UNCHIN_IMP_FROM_DATE") = .lblUnchinImpDate.TextValue
            dr.Item("SAGYO_IMP_FROM_DATE") = .lblSagyoImpDate.TextValue
            dr.Item("YOKOMOCHI_IMP_FROM_DATE") = .lblYokomochiImpDate.TextValue
            dr.Item("REMARK") = .txtRemark.TextValue
            dr.Item("RB_FLG") = LMG050C.KURO_DEN
            dr.Item("STORAGE_KB") = Me.ExistImportData(LMG050C.SKYU_GROUP_HOKAN)
            dr.Item("HANDLING_KB") = Me.ExistImportData(LMG050C.SKYU_GROUP_NIYAKU)
            dr.Item("UNCHIN_KB") = Me.ExistImportData(LMG050C.SKYU_GROUP_UNCHIN)
            dr.Item("SAGYO_KB") = Me.ExistImportData(LMG050C.SKYU_GROUP_SAGYO)
            dr.Item("YOKOMOCHI_KB") = Me.ExistImportData(LMG050C.SKYU_GROUP_YOKOMOCHI)
            ''2014.08.21 追加START 多通貨対応
            dr.Item("INV_CURR_CD") = .cmbSeiqCurrCd.SelectedValue
            dr.Item("EX_RATE") = .numExRate.Value
            dr.Item("EX_MOTO_CURR_CD") = .cmbCurrencyConversion1.SelectedValue
            dr.Item("EX_SAKI_CURR_CD") = .cmbCurrencyConversion2.SelectedValue
            ''2014.08.21 追加END 多通貨対応

            '2014.08.21 追加開始 要望番号:2286対応
            dr.Item("UNSO_WT") = Convert.ToDecimal(.numUnsoWT.Value)
            '2014.08.21 追加終了 要望番号:2286対応

            '排他制御用
            dr.Item("SYS_UPD_DATE") = .lblSysUpdDate.TextValue
            dr.Item("SYS_UPD_TIME") = .lblSysUpdTime.TextValue

        End With

        dt.Rows.Add(dr)

        '★ DEL START 2011/09/06 SUGA
        ''明細部の項目格納
        'Call Me.SetDataSetSaveDtl(ds)
        '★ DEL E N D 2011/09/06 SUGA

    End Sub

    ''' <summary>
    ''' 取込データが明細に存在するか判断する
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ExistImportData(ByVal chkGroup As String) As String

        Dim rtnString As String = LMG050C.MITORIKOMI

        With Me._Frm.sprSeikyuM.ActiveSheet

            Dim max As Integer = .Rows.Count - 1
            Dim group As String = String.Empty

            For i As Integer = 0 To max
                If Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.CREATE_SYUBETU_KBN.ColNo)).Equals(LMGControlC.DETAIL_SAKUSEI_AUTO) _
                AndAlso Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.TEMPLATE_IMP_FLG.ColNo)).Equals(LMGControlC.YN_FLG_NO) Then

                    If Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.GROUP_CD_KBN.ColNo)).Equals(chkGroup) Then
                        rtnString = LMG050C.TORIKOMI_ZUMI
                        Exit For
                    End If
                End If
            Next

        End With

        Return rtnString

    End Function

    '★ UPD START 2011/09/06 SUGA
    ''' <summary>
    ''' 入力内容をDataSetに格納する(明細)
    ''' </summary>
    ''' <param name="ds">更新内容を格納するデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetSaveDtl(ByVal ds As DataSet, Optional ByVal saveFlg As Boolean = False)

        Dim dt As DataTable = Me._FindDs.Tables(LMG050C.TABLE_NM_DTL)
        Dim dr As DataRow = Nothing
        Dim recNo As Integer = 0
        Dim max As Integer = 0
        Dim edaNo As Integer = Me._EdaNo
        Dim nrsBrCd As String = Me._Frm.cmbBr.SelectedValue.ToString()

        With Me._Frm.sprSeikyuM.ActiveSheet

            max = .Rows.Count - 1
            For i As Integer = 0 To max

                recNo = Convert.ToInt32(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.RECORD_NO.ColNo)))
                dr = dt.Rows(recNo)

                dr.Item("NRS_BR_CD") = nrsBrCd
                dr.Item("BUSYO_CD") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.BUSYO.ColNo))
                dr.Item("KEISAN_TLGK") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.KEISAN_GAKU.ColNo))
                dr.Item("NEBIKI_RT") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.NEBIKI_RATE.ColNo))
                dr.Item("NEBIKI_GK") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo))
                dr.Item("TEKIYO") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.TEKIYOU.ColNo))
                dr.Item("PRINT_SORT") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.IN_JUN.ColNo))

                '2014.08.21 追加START 多通貨対応
                dr.Item("ITEM_CURR_CD") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.ITEM_CURR_CD.ColNo))
                dr.Item("EX_RATE") = Me._Frm.numExRate.Value
                '2014.08.21 追加END   多通貨対応

                '★ ADD START 2011/09/06 SUGA
                dr.Item("NEBIKI_RTGK") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.RATENEBIKI_GAKU.ColNo))
                '★ ADD E N D 2011/09/06 SUGA

                '　ADD START 2014/02/24 SHINODA TAX_CD_JDEを追加
                Dim tax_kb As String = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.KAZEI_KBN.ColNo))
                Dim filter As String = String.Empty
                Dim tax_cd_jde As String = String.Empty
                filter = String.Concat(filter, "SYS_DEL_FLG = '0'")
                filter = String.Concat(filter, " AND KBN_GROUP_CD = '", LMKbnConst.KBN_Z001, "' ")
                filter = String.Concat(filter, " AND KBN_CD = '", tax_kb, "' ")
                '区分マスタより、税コードを取得
                Dim kbnDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
                If kbnDr.Count > 0 Then
                    Dim taxCd As String = kbnDr(0).Item("KBN_NM3").ToString
                    '税マスタより、JDE用TAX_CDを取得
                    filter = String.Empty
                    filter = String.Concat(filter, "SYS_DEL_FLG = '0'")
                    filter = String.Concat(filter, " AND TAX_CD = '", taxCd, "' ")
                    Dim Start_Data As String = Me._Frm.imdInvDate.TextValue
                    '請求日に値が入力されていなかった場合
                    If String.IsNullOrEmpty(Start_Data) = True Then
                        filter = String.Concat(filter, " AND START_DATE <= '", MyBase.GetSystemDateTime(0), "' ")
                    Else
                        '請求日に値が入力されている場合
                        filter = String.Concat(filter, " AND START_DATE <= '", Start_Data, "' ")
                    End If
                    Dim taxDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TAX).Select(filter, "START_DATE desc")
                    If taxDr.Count > 0 Then
                        tax_cd_jde = taxDr(0).Item("TAX_CD_JDE").ToString
                    Else
                        tax_cd_jde = taxCd
                    End If

                End If
                dr.Item("TAX_CD_JDE") = tax_cd_jde
                '　ADD E N D 2014/02/24 SHINODA TAX_CD_JDEを追加

                'ADD 2016/09/06 Start 最保管貨対応
                dr.Item("JISYATASYA_KB") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.JISYATASYA_KB.ColNo))
                'ADD 2016/09/06 End
                dr.Item("PRODUCT_SEG_CD") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.PRODUCT_SEG_CD.ColNo))
                dr.Item("ORIG_SEG_CD") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.ORIG_SEG_CD.ColNo))
                dr.Item("DEST_SEG_CD") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.DEST_SEG_CD.ColNo))
                dr.Item("TCUST_BPCD") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.TCUST_BPCD.ColNo))
                dr.Item("SEIQKMK_CD_S") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.SEIQKMK_CD_S.ColNo))

                '保存時のみ枝番を新規採番する
                If saveFlg AndAlso String.IsNullOrEmpty(dr.Item("SKYU_SUB_NO").ToString()) Then
                    edaNo = edaNo + 1
                    dr.Item("SKYU_SUB_NO") = edaNo.ToString().PadLeft(2, Convert.ToChar("0"))
                End If
            Next

        End With

        max = dt.Rows.Count - 1
        For i As Integer = 0 To max
            ds.Tables(LMG050C.TABLE_NM_DTL).ImportRow(dt.Rows(i))
        Next

    End Sub
    '★ UPD E N D 2011/09/06 SUGA

    ''' <summary>
    ''' 入力内容をDataSetに格納する(行追加時(明細))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetAddDtl()

        Dim dt As DataTable = Me._FindDs.Tables(LMG050C.TABLE_NM_DTL)
        Dim dr As DataRow = Nothing
        Dim recNo As Integer = 0

        With Me._Frm.sprSeikyuM.ActiveSheet

            Dim max As Integer = .Rows.Count - 1
            For i As Integer = 0 To max

                recNo = Convert.ToInt32(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.RECORD_NO.ColNo)))
                dr = dt.Rows(recNo)

                dr.Item("BUSYO_CD") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.BUSYO.ColNo))
                dr.Item("KEISAN_TLGK") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.KEISAN_GAKU.ColNo))
                dr.Item("NEBIKI_RT") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.NEBIKI_RATE.ColNo))
                dr.Item("NEBIKI_GK") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo))
                dr.Item("TEKIYO") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.TEKIYOU.ColNo))
                dr.Item("PRINT_SORT") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.IN_JUN.ColNo))
                dr.Item("PRODUCT_SEG_CD") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.PRODUCT_SEG_CD.ColNo))
                dr.Item("ORIG_SEG_CD") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.ORIG_SEG_CD.ColNo))
                dr.Item("DEST_SEG_CD") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.DEST_SEG_CD.ColNo))
                dr.Item("TCUST_BPCD") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.TCUST_BPCD.ColNo))
                dr.Item("TCUST_BPNM") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.TCUST_BPNM.ColNo))
                dr.Item("SEIQKMK_CD_S") = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.SEIQKMK_CD_S.ColNo))
                '2014.08.21 修正START 多通貨対応
                '１行目の契約建値と小数点桁数を設定
                dr.Item("ITEM_CURR_CD") = dt.Rows(0).Item("ITEM_CURR_CD").ToString()
                dr.Item("ROUND_POS") = Convert.ToDecimal(dt.Rows(0).Item("ROUND_POS"))
                '2014.08.21 修正END   多通貨対応

            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(ステージ更新用)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="eventShubetu"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSetStageUp(ByVal ds As DataSet _
                                   , ByVal eventShubetu As LMG050C.EventShubetsu)

        '基本項目設定
        Call Me.SetDataSetEventData(ds)

        Dim dt As DataTable = ds.Tables(LMG050C.TABLE_NM_HED)

        Dim state As String = String.Empty
        Select Case eventShubetu
            Case LMG050C.EventShubetsu.INITIALIZE        'F7(初期化)　    ⇒ "00"(未確定)
                state = LMG050C.STATE_MIKAKUTEI

                dt.Rows(0).Item("UNCHIN_IMP_FROM_DATE") = Me._Frm.lblUnchinImpDate.TextValue
                dt.Rows(0).Item("YOKOMOCHI_IMP_FROM_DATE") = Me._Frm.lblYokomochiImpDate.TextValue
                dt.Rows(0).Item("SAGYO_IMP_FROM_DATE") = Me._Frm.lblSagyoImpDate.TextValue

                If String.IsNullOrEmpty(Me._Frm.lblSeikyuNoRelated.TextValue) Then

                    If Me._Frm.chkUnchin.GetBinaryValue().Equals(LMConst.FLG.ON) Then
                        dt.Rows(0).Item("UNCHIN_IMP_FROM_DATE") = String.Empty
                    End If
                    If Me._Frm.chkYokomochi.GetBinaryValue().Equals(LMConst.FLG.ON) Then
                        dt.Rows(0).Item("YOKOMOCHI_IMP_FROM_DATE") = String.Empty
                    End If
                    If Me._Frm.chkSagyou.GetBinaryValue().Equals(LMConst.FLG.ON) Then
                        dt.Rows(0).Item("SAGYO_IMP_FROM_DATE") = String.Empty
                    End If

                End If

            Case LMG050C.EventShubetsu.KAKUTEI          'F5(確定)        ⇒ "01"(確定)
                state = LMG050C.STATE_KAKUTEI

                Select Case Me._Frm.cmbSeiqtShubetu.SelectedValue.ToString()
                    Case LMGControlC.CRT_TORIKOMI

                        dt.Rows(0).Item("UNCHIN_IMP_FROM_DATE") = Me._Frm.lblUnchinImpDate.TextValue
                        dt.Rows(0).Item("YOKOMOCHI_IMP_FROM_DATE") = Me._Frm.lblYokomochiImpDate.TextValue
                        dt.Rows(0).Item("SAGYO_IMP_FROM_DATE") = Me._Frm.lblSagyoImpDate.TextValue

                        If String.IsNullOrEmpty(Me._Frm.lblSeikyuNoRelated.TextValue) Then
                            If String.IsNullOrEmpty(Me._Frm.lblUnchinImpDate.TextValue) _
                                OrElse String.IsNullOrEmpty(Me._Frm.lblYokomochiImpDate.TextValue) _
                                OrElse String.IsNullOrEmpty(Me._Frm.lblSagyoImpDate.TextValue) Then
                                '対象データを検索する

                                Dim comDs As DataSet = New LMG000DS()
                                Dim comDt As DataTable = comDs.Tables(LMGControlC.TABLE_NM_GET_INV_IN)
                                Dim comDr As DataRow = comDt.NewRow()
                                'Dim SeqCd As ArrayList = Me._ControlG.GetSeqNm(Me._Frm.txtSeikyuCd.TextValue)
                                '20160927 要番2622 tsunehira add Start
                                Dim SeqCd As ArrayList = Me._ControlG.GetSeqNm(Me._Frm.cmbBr.SelectedValue.ToString, Me._Frm.txtSeikyuCd.TextValue)
                                '20160927 要番2622 tsunehira add End

                                If SeqCd.Count >= 1 Then
                                    comDr.Item("SEIQTO_CD") = SeqCd(1).ToString()
                                End If
                                comDr.Item("SKYU_DATE") = Me._Frm.imdInvDate.TextValue
                                comDr.Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue
                                comDt.Rows.Add(comDr)

                                comDs = MyBase.CallWSA("LMG050BLF", "GetInvFrom", comDs)

                                Dim invFrom As String = comDs.Tables(LMGControlC.TABLE_NM_GET_INV_OUT).Rows(0).Item("SKYU_DATE_FROM").ToString()
                                If String.IsNullOrEmpty(invFrom) Then
                                    invFrom = "00000000"
                                Else
                                    invFrom = Date.ParseExact(invFrom, "yyyyMMdd", Nothing).AddDays(1).ToString.Replace("/", "").Substring(0, 8)
                                End If

                                If String.IsNullOrEmpty(Me._Frm.lblUnchinImpDate.TextValue) Then
                                    dt.Rows(0).Item("UNCHIN_IMP_FROM_DATE") = invFrom
                                End If
                                If String.IsNullOrEmpty(Me._Frm.lblYokomochiImpDate.TextValue) Then
                                    dt.Rows(0).Item("YOKOMOCHI_IMP_FROM_DATE") = invFrom
                                End If
                                If String.IsNullOrEmpty(Me._Frm.lblSagyoImpDate.TextValue) Then
                                    dt.Rows(0).Item("SAGYO_IMP_FROM_DATE") = invFrom
                                End If
                            End If

                        End If

                End Select

            Case LMG050C.EventShubetsu.PRINT            '印刷            ⇒ "02"(印刷済)
                state = LMG050C.STATE_INSATU_ZUMI

            Case LMG050C.EventShubetsu.KEIRITAISHOGAI   'F8(経理対象外)　⇒ "04"(経理対象外)
                state = LMG050C.STATE_KEIRI_TAISHO_GAI

        End Select

        dt.Rows(0).Item("STATE_KB") = state

    End Sub

    ''' <summary>
    ''' 削除行を格納用DataSetに反映する
    ''' </summary>
    ''' <param name="list">チェック行格納配列</param>
    ''' <remarks></remarks>
    Private Sub SetDataDeleateRow(ByVal list As ArrayList)

        '編集内容を保持する
        Call Me.SetDataSetAddDtl()

        Dim dt As DataTable = Me._FindDs.Tables(LMG050C.TABLE_NM_DTL)
        Dim dr As DataRow = Nothing
        Dim recNo As Integer = 0

        With Me._Frm.sprSeikyuM.ActiveSheet

            Dim max As Integer = .Rows.Count - 1
            Dim chkMax As Integer = list.Count - 1
            Dim delFlg As Boolean = False
            For i As Integer = max To 0 Step -1

                '削除フラグ初期化
                delFlg = False

                For chkR As Integer = 0 To chkMax
                    If Convert.ToInt32(list(chkR)) = i Then
                        delFlg = True
                        Exit For
                    End If
                Next

                If delFlg = True Then

                    recNo = Convert.ToInt32(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.RECORD_NO.ColNo)))
                    dr = dt.Rows(recNo)

                    If dr.Item("INS_FLG").Equals(LMConst.FLG.ON) Then
                        '物理削除
                        dt.Rows.Remove(dr)
                        Me._Frm.lblMaxEdaban.TextValue = (Convert.ToInt32(Me._Frm.lblMaxEdaban.TextValue) - 1).ToString()
                    Else
                        '論理削除
                        dr.Item("SYS_DEL_FLG") = LMConst.FLG.ON
                    End If

                End If


            Next

            'レコードNO再設定
            max = dt.Rows.Count - 1
            For i As Integer = 0 To max

                dt(i).Item("RECORD_NO") = i.ToString().PadLeft(2, Convert.ToChar("0"))
            Next

        End With

    End Sub

    ''' <summary>
    '''以前取り込んだデータを削除する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteImportRow()

        Dim dt As DataTable = Me._FindDs.Tables(LMG050C.TABLE_NM_DTL)

        Dim max As Integer = dt.Rows.Count - 1
        Dim delFlg As Boolean = False
        For i As Integer = max To 0 Step -1

            With dt.Rows(i)

                '削除フラグ初期化
                delFlg = False

                If .Item("MAKE_SYU_KB").ToString().Equals(LMGControlC.DETAIL_SAKUSEI_AUTO) Then
                    delFlg = True
                End If

                If delFlg = True Then
                    Select Case .Item("TEMPLATE_IMP_FLG").ToString()
                        Case "00"
                            Select Case .Item("GROUP_KB").ToString()
                                Case LMG050C.SKYU_GROUP_HOKAN
                                    If Me._Frm.chkHokan.GetBinaryValue().Equals(LMConst.FLG.OFF) Then
                                        delFlg = False
                                    End If
                                Case LMG050C.SKYU_GROUP_NIYAKU
                                    If Me._Frm.chkNiyaku.GetBinaryValue().Equals(LMConst.FLG.OFF) Then
                                        delFlg = False
                                    End If
                                Case LMG050C.SKYU_GROUP_UNCHIN
                                    If Me._Frm.chkUnchin.GetBinaryValue().Equals(LMConst.FLG.OFF) Then
                                        delFlg = False
                                    End If
                                Case LMG050C.SKYU_GROUP_SAGYO
                                    If Me._Frm.chkSagyou.GetBinaryValue().Equals(LMConst.FLG.OFF) Then
                                        delFlg = False
                                    End If
                                Case LMG050C.SKYU_GROUP_YOKOMOCHI
                                    If Me._Frm.chkYokomochi.GetBinaryValue().Equals(LMConst.FLG.OFF) Then
                                        delFlg = False
                                    End If
                                Case LMG050C.SKYU_GROUP_DEPOT_HOKAN
                                    If Me._Frm.chkDepotHokan.GetBinaryValue().Equals(LMConst.FLG.OFF) Then
                                        delFlg = False
                                    End If
                                Case LMG050C.SKYU_GROUP_DEPOT_LIFT
                                    If Me._Frm.chkDepotLift.GetBinaryValue().Equals(LMConst.FLG.OFF) Then
                                        delFlg = False
                                    End If
                                Case LMG050C.SKYU_GROUP_CONTAINER_UNSO
                                    If Me._Frm.chkContainerUnso.GetBinaryValue().Equals(LMConst.FLG.OFF) Then
                                        delFlg = False
                                    End If
                            End Select
                        Case "01"
                            If Me._Frm.chkTemplate.GetBinaryValue().Equals(LMConst.FLG.OFF) Then
                                delFlg = False
                            End If
                    End Select

                    If LMGControlC.DETAIL_SAKUSEI_AUTO.Equals(.Item("MAKE_SYU_KB").ToString()) _
                        AndAlso LMG050C.SKYU_GROUP_HOKAN.Equals(.Item("GROUP_KB").ToString()) _
                        AndAlso LMGControlC.SEIQKMK_GYOMUITAKU_JIMU.Equals(.Item("SEIQKMK_CD").ToString()) Then
                        delFlg = False
                    End If

                End If

                If delFlg = True Then
                    If .Item("INS_FLG").Equals(LMConst.FLG.ON) Then
                        '物理削除
                        dt.Rows.Remove(dt.Rows(i))
                        Me._Frm.lblMaxEdaban.TextValue = (Convert.ToInt32(Me._Frm.lblMaxEdaban.TextValue) - 1).ToString()
                    Else
                        '論理削除
                        .Item("SYS_DEL_FLG") = LMConst.FLG.ON
                    End If

                End If

            End With

        Next

        'レコードNO再設定
        max = dt.Rows.Count - 1
        For i As Integer = 0 To max

            dt(i).Item("RECORD_NO") = i.ToString().PadLeft(2, Convert.ToChar("0"))
        Next

    End Sub

    ''' <summary>
    ''' フォーカス設定
    ''' </summary>
    ''' <param name="focusNoMove">フォーカス移動したくない際はTrueを指定</param>
    Private Sub SetFocus(Optional ByVal focusNoMove As Boolean = False)

        'イベント解除
        Call Me.RemoveEventHandler()

        'フォーカス設定
        If Not focusNoMove Then
            Call Me._G.SetFocus()
        End If

        'イベント設定
        Call Me.SetEventHandler()

    End Sub

    ''' <summary>
    ''' ヘッダ部Leaveイベントを設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetEventHandler()

        '課税計算イベント
        AddHandler Me._Frm.numNebikiRateK.Leave, AddressOf Kazei_Leave
        AddHandler Me._Frm.numNebikiGakuK.Leave, AddressOf Kazei_Leave
        AddHandler Me._Frm.numZeiHasuK.Leave, AddressOf Kazei_Leave

        '免税計算イベント
        AddHandler Me._Frm.numNebikiRateM.Leave, AddressOf Menzei_Leave
        AddHandler Me._Frm.numNebikiGakuM.Leave, AddressOf Menzei_Leave

        '請求日イベント
        AddHandler Me._Frm.imdInvDate.Leave, AddressOf SeiqtoDay_Leave

        '請求先コードイベント
        AddHandler Me._Frm.txtSeikyuCd.Leave, AddressOf txtSeikyuCd_Leave

    End Sub

    ''' <summary>
    ''' ヘッダ部Leaveイベントを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveEventHandler()

        '課税計算イベント
        RemoveHandler Me._Frm.numNebikiRateK.Leave, AddressOf Kazei_Leave
        RemoveHandler Me._Frm.numNebikiGakuK.Leave, AddressOf Kazei_Leave
        RemoveHandler Me._Frm.numZeiHasuK.Leave, AddressOf Kazei_Leave

        '免税計算イベント
        RemoveHandler Me._Frm.numNebikiRateM.Leave, AddressOf Menzei_Leave
        RemoveHandler Me._Frm.numNebikiGakuM.Leave, AddressOf Menzei_Leave

        '請求日イベント
        RemoveHandler Me._Frm.imdInvDate.Leave, AddressOf SeiqtoDay_Leave

        '請求先コードイベント
        RemoveHandler Me._Frm.txtSeikyuCd.Leave, AddressOf txtSeikyuCd_Leave

    End Sub

    ''' <summary>
    ''' ログインユーザの部署コードを取得する
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetLoginUserBusyoCd() As String

        Dim busyo As String = String.Empty
        'START YANAI 要望番号831
        'Dim filter As String = String.Empty
        'filter = String.Concat(filter, "KBN_GROUP_CD = '", LMKbnConst.KBN_B007, "'")
        'filter = String.Concat(filter, " AND KBN_NM2 = '", LMUserInfoManager.GetNrsBrCd, "'")
        'filter = String.Concat(filter, " AND KBN_NM3 = '1' AND SYS_DEL_FLG = '0'")
        'Dim busyoDr As DataRow() = GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
        'If busyoDr.Length > 0 Then
        '    busyo = busyoDr(0).Item("KBN_CD").ToString()
        'End If
        Dim busyoDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", MyBase.GetUserID, "'"))
        If 0 < busyoDr.Length Then
            busyo = busyoDr(0).Item("BUSYO_CD").ToString
        End If
        'END YANAI 要望番号831

        Return busyo

    End Function

    ''' <summary>
    ''' 適用開始日取得処理
    ''' </summary>
    ''' <param name="brCd"></param>
    ''' <param name="seiqtoCd"></param>
    ''' <param name="seiqDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetStartDate(ByVal brCd As String, ByVal seiqtoCd As String, ByVal seiqDate As String) As String

        '対象データを検索する
        Dim invDs As DataSet = New LMG000DS()
        Dim invDt As DataTable = invDs.Tables(LMGControlC.TABLE_NM_GET_INV_IN)
        Dim invDr As DataRow = invDt.NewRow()
        invDr.Item("SEIQTO_CD") = seiqtoCd
        invDr.Item("SKYU_DATE") = seiqDate
        invDr.Item("NRS_BR_CD") = brCd
        invDt.Rows.Add(invDr)

        invDs = MyBase.CallWSA("LMG050BLF", "GetInvFrom", invDs)

        '取得した請求開始日を設定
        Dim startDate As String = String.Empty
        Dim getDate As String = invDs.Tables(LMGControlC.TABLE_NM_GET_INV_OUT).Rows(0)("SKYU_DATE_FROM").ToString()
        If String.IsNullOrEmpty(getDate) Then
            startDate = "00000000"
        Else
            startDate = Date.ParseExact(getDate, "yyyyMMdd", Nothing).AddDays(1).ToString.Replace("/", "").Substring(0, 8)
        End If

        Return startDate

    End Function

    ''' <summary>
    ''' 請求明細印刷判断
    ''' </summary>
    ''' <param name="meisaiKbn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMeisaiPrintFlg(ByVal meisaiKbn As LMG050C.IkkatsuPrintMeisaiKbn) As String

        Dim printKbn As String = LMG050C.PRINT_NOT

        With Me._Frm.sprSeikyuM.ActiveSheet
            For i As Integer = 0 To .RowCount - 1
                '一件でも自動取込の明細があれば印刷対象
                If LMG050C.MAKE_SYU_KB_AUTO.Equals(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.CREATE_SYUBETU_KBN.ColNo))) = True Then
                    Select Case meisaiKbn
                        Case LMG050C.IkkatsuPrintMeisaiKbn.HokanNiyaku
                            If LMG050C.SKYU_GROUP_HOKAN.Equals(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.GROUP_CD_KBN.ColNo))) = True _
                                 OrElse LMG050C.SKYU_GROUP_NIYAKU.Equals(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.GROUP_CD_KBN.ColNo))) = True Then
                                printKbn = LMG050C.PRINT_DO
                                Exit For
                            End If

                        Case LMG050C.IkkatsuPrintMeisaiKbn.Unchin
                            If LMG050C.SKYU_GROUP_UNCHIN.Equals(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.GROUP_CD_KBN.ColNo))) = True _
                                 OrElse LMG050C.SKYU_GROUP_YOKOMOCHI.Equals(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.GROUP_CD_KBN.ColNo))) = True Then
                                printKbn = LMG050C.PRINT_DO
                                Exit For
                            End If

                        Case LMG050C.IkkatsuPrintMeisaiKbn.Sagyo
                            If LMG050C.SKYU_GROUP_SAGYO.Equals(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.GROUP_CD_KBN.ColNo))) = True Then
                                printKbn = LMG050C.PRINT_DO
                                Exit For
                            End If

                    End Select

                End If
            Next

        End With

        Return printKbn

    End Function

    ''' <summary>
    ''' 運賃締め基準の取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SimeKijun(ByVal brCd As String, ByVal seiqCd As String) As String

        Dim sime As String = "01"

        Dim drC As DataRow() = Nothing
        Dim count As Integer = 0

        'キャッシュの荷主マスターの値の取得
        drC = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("SYS_DEL_FLG = '0' AND NRS_BR_CD = '", brCd, "' AND UNCHIN_SEIQTO_CD = '", seiqCd, "'"))

        'データロウのカウントの取得
        count = drC.Count

        'データが1件以上の場合
        If count >= 1 Then

            'データロウの0の運賃締め基準の取得
            sime = drC(0).Item("UNTIN_CALCULATION_KB").ToString

        End If

        Return sime
    End Function

    ''' <summary>
    ''' 真荷主コードの存在チェック
    ''' </summary>
    ''' <returns></returns>
    Private Function TCustBpChk() As Boolean

        With Me._Frm.sprSeikyuM.ActiveSheet

            For i As Integer = 0 To .Rows.Count - 1
                Dim tCustBpCd As String = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.TCUST_BPCD.ColNo))

                'Pop起動処理：画面表示なし
                Me._PopupSkipFlg = False

                '真荷主参照POP起動
                If Not Me.ShowTcustPopup(Me._Frm, i, LMG050C.EventShubetsu.SAVE) Then
                    Call Me._ControlV.SetErrorControl(Me._Frm.sprSeikyuM, i, LMG050G.sprSeikyuMDef.TCUST_BPCD.ColNo)
                    MyBase.ShowMessage(Me._Frm, "E078", New String() {"真荷主マスタ", ""})
                    Return False
                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 請求先判定(TSMCか否か)
    ''' </summary>
    ''' <returns></returns>
    Private Function IsTsmc() As Boolean

        Try

            Dim ds As DataSet = New LMG050DS()
            Dim dt As DataTable = ds.Tables(LMG050C.TABLE_NM_IN_TSMC)
            Dim dr As DataRow = dt.NewRow

            With Me._Frm

                If .txtSeikyuCd.TextValue() = "" Then
                    ' 請求先コードが未入力ならば TSMC ではないと判定する。
                    Return False
                End If

                If Me._SeikyuCdIsTsmc = .txtSeikyuCd.TextValue() Then
                    ' 請求先コードが前回と変わらない場合
                    ' 判定も前回と同じ
                    Return Me._IsTsmc
                End If

                ' 営業所コード
                dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue.ToString()
                ' 請求先コード
                dr.Item("SEIQTO_CD") = .txtSeikyuCd.TextValue()

                dt.Rows.Add(dr)

            End With

            ' セット料金の単価マスタが登録された荷主の主請求先(であるか否か) の検索
            Dim rtnDs As DataSet = MyBase.CallWSA("LMG050BLF", "SelectSeiqtoCustSetPrice", ds)

            If MyBase.GetResultCount() = 0 Then
                Return False
            End If

            ' セット料金の単価マスタが登録された荷主の主請求先であれば TSMC と判定する。
            Return True
        Finally
            With Me._Frm
                ' 判定に使用する画面項目よりの値の退避

                ' 請求先コード
                Me._SeikyuCdIsTsmc = .txtSeikyuCd.TextValue()

            End With

        End Try

    End Function

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMG050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditEvent")

        '編集
        Call Me.EditEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditEvent")

    End Sub

#If True Then   ''ADD 2018/08/21 依頼番号 : 002136 
    ''' <summary>
    ''' F3押下時処理呼び出し(復活削除)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMG050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FukkatsuEvent")

        '復活
        Call Me.FukkatsuEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FukkatsuEvent")

    End Sub
#End If

    ''' <summary>
    ''' F4押下時処理呼び出し(削除)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMG050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteEvent")

        '削除
        Call Me.DeleteEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteEvent")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し(確定)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMG050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "KakuteiEvent")

        '確定
        Call Me.KakuteiEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "KakuteiEvent")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し(取込)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMG050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ImportEvent")

        '取込
        Call Me.ImportEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ImportEvent")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し(初期化)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMG050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "InitializeEvent")

        '初期化
        Call Me.InitializeEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "InitializeEvent")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し(経理対象外)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMG050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "KeiriTaishoGaiEvent")

        '経理対象外
        Call Me.KeiriTaishoGaiEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "KeiriTaishoGaiEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(経理戻し)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMG050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "KeiriModoshiEvent")

        '経理戻し
        Call Me.KeiriModoshiEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "KeiriModoshiEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMG050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

        'マスタ参照
        Me.MasterShowEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMG050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveEvent")

        '保存
        Me.SaveEvent(LMG050C.EventShubetsu.SAVE)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMG050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey12Press")

        '終了処理  
        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMG050F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '閉じる処理
        Call Me.CloseFormEvent(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMG050FKeyDown(ByVal frm As LMG050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'エンター押下
        Call Me.EnterAction(e)

    End Sub

    ''' <summary>
    ''' Addボタン押下時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub AddClick(ByVal frm As LMG050F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "AddClick")

        '行追加処理
        Call Me.RowAdd()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "AddClick")

    End Sub

    ''' <summary>
    ''' Deleteボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub DelClick(ByVal frm As LMG050F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DelClick")

        '行削除処理
        Call Me.RowDel()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DelClick")

    End Sub

    ''' <summary>
    ''' SAP出力ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub SapOutClick(ByVal frm As LMG050F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SapOutClick")

        ' SAP出力処理
        Call Me.BtnSapOutClick()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SapOutClick")

    End Sub

    ''' <summary>
    ''' SAP取消ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub SapCancelClick(ByVal frm As LMG050F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SapCancelClick")

        ' SAP出力処理
        Call Me.BtnSapCancelClick()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SapCancelClick")

    End Sub

    ''' <summary>
    ''' Printボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub PrintClick(ByVal frm As LMG050F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintClick")

        '印刷処理
        Call Me.BtnPrintClick()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "PrintClick")

    End Sub

    ''' <summary>
    ''' セルのLeaveイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub LeaveCell(ByVal frm As LMG050F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LeaveCell")

        'セルのLeaveイベントを行う
        Call Me.LeaveSprCell(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LeaveCell")

    End Sub

    ''' <summary>
    ''' 課税分Leaveイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Kazei_Leave(ByVal sender As Object, ByVal e As System.EventArgs)

        Call Me.LeaveKazei(e)

    End Sub

    ''' <summary>
    ''' 免税分Leaveイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Menzei_Leave(ByVal sender As Object, ByVal e As System.EventArgs)

        Call Me.LeaveMenzei(e)

    End Sub

    ''' <summary>
    ''' 請求日Leaveイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SeiqtoDay_Leave(ByVal sender As Object, ByVal e As System.EventArgs)

        Call Me.LeaveSeiqtoDay(e)

    End Sub

    ''' <summary>
    ''' 集計部(課税分)Leaveイベント
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub LeaveKazei(ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LeaveKazei")

        Call Me.LeaveHedCalc(LMGControlC.TAX_KAZEI)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LeaveKazei")

    End Sub

    ''' <summary>
    ''' 集計部(免税分)Leaveイベント
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub LeaveMenzei(ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LeaveMenzei")

        Call Me.LeaveHedCalc(LMGControlC.TAX_MENZEI)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LeaveMenzei")

    End Sub

    ''' <summary>
    ''' 請求日のLeaveイベント
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub LeaveSeiqtoDay(ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SeiqtoDay")

        '課税区分から税率を取得
        Call Me.LeaveHedCalc(LMGControlC.TAX_KAZEI)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SeiqtoDay")

    End Sub


    Private Sub txtSeikyuCd_Leave(ByVal sender As Object, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "txtSeikyuCd_Leave")

        'Leave処理
        Call Me.LeavetxtSeikyuCd()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "txtSeikyuCd_Leave")

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub txtSeikyuCdGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "txtSeikyuCdGotFocus")

        'GotFocus処理
        Call Me.GotFocustxtSeikyuCd()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "txtSeikyuCdGotFocus")

    End Sub

    '2014.08.21 追加START 多通貨対応
    ''' <summary>
    ''' 請求建値変更時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub CmbSeiqCurrLeave(ByVal frm As LMG050F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CmbSeiqCurrLeave")

        '請求建値ロストフォーカス処理
        Call Me.ControlLeave()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CmbSeiqCurrLeave")

    End Sub
    '2014.08.21 追加END 多通貨対応

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SelectSeiqtoData(ByVal ds As DataSet) As Boolean

        ''SPREAD(表示行)初期化
        'Me._Frm.sprSeikyuM.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectSeiqtoData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMG050BLF", "SelectSeiqtoData", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectSeiqtoData")

        '明細データ格納(更新時使用)

        Dim dtlDt As DataTable = Me._FindDs.Tables(LMG050C.TABLE_NM_DTL)
        Dim seiqDt As DataTable = rtnDs.Tables(LMG050C.TABLE_NM_SEIQTO)


        If seiqDt.Rows.Count > 0 Then

            Dim minAmountMst As Long = CLng(seiqDt.Rows(0).Item("TOTAL_MIN_SEIQ_AMT"))
            Dim minAmount As Long = 0
            Dim sumAmount As Long = 0
            Dim minFlg As Boolean = False

            Dim groupKb As String = String.Empty
            Dim sqKmkCd As String = String.Empty
            Dim busyoCd As String = String.Empty
            Dim autoKb As String = String.Empty
            Dim rtnFlg As Boolean = False
            Dim rowNo As Integer = 0
            Dim inputAmt As Long = 0

            With Me._Frm.sprSeikyuM.ActiveSheet
                Dim max As Integer = .Rows.Count - 1

                For i As Integer = 0 To max

                    rowNo = i

                    '部署チェックを行う
                    groupKb = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.GROUP_CD_KBN.ColNo))
                    sqKmkCd = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.SHUBETU_KBN.ColNo))
                    busyoCd = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.BUSYO.ColNo))
                    inputAmt = CLng(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.KEISAN_GAKU.ColNo)))
                    autoKb = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.CREATE_SYUBETU_KBN.ColNo))

                    '自動作成で生きているものだけを修正
                    If LMGControlC.DETAIL_SAKUSEI_AUTO.Equals(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.CREATE_SYUBETU_KBN.ColNo))) Then


                        '保管料
                        If LMGControlC.STORQAGE_FEE1.Equals(groupKb) _
                            AndAlso Not LMGControlC.SEIQKMK_GYOMUITAKU_JIMU.Equals(sqKmkCd) _
                            AndAlso LMConst.FLG.ON.Equals(seiqDt.Rows(0).Item("STORAGE_TOTAL_FLG")) Then
                            sumAmount = sumAmount + inputAmt
                            'minFlg = True
                        End If
                        '荷役料
                        If LMGControlC.HANDLING_CHARGE1.Equals(groupKb) _
                            AndAlso LMConst.FLG.ON.Equals(seiqDt.Rows(0).Item("HANDLING_TOTAL_FLG")) Then
                            sumAmount = sumAmount + inputAmt
                            'minFlg = True
                        End If
                        '運賃量
                        If LMGControlC.FREIGHT_TAXATION1.Equals(groupKb) _
                            AndAlso LMConst.FLG.ON.Equals(seiqDt.Rows(0).Item("UNCHIN_TOTAL_FLG")) Then
                            sumAmount = sumAmount + inputAmt
                            'minFlg = True
                        End If
                        '作業量
                        If LMGControlC.WORK_FEE1.Equals(groupKb) _
                            AndAlso LMConst.FLG.ON.Equals(seiqDt.Rows(0).Item("SAGYO_TOTAL_FLG")) Then
                            sumAmount = sumAmount + inputAmt
                            'minFlg = True
                        End If
                        '鑑最低保証
                        If LMGControlC.STORQAGE_FEE1.Equals(groupKb) _
                            AndAlso LMGControlC.SEIQKMK_GYOMUITAKU_JIMU.Equals(sqKmkCd) _
                            AndAlso LMGControlC.DETAIL_SAKUSEI_AUTO.Equals(autoKb) Then
                            minAmount = inputAmt
                            minFlg = True
                        End If
                    End If
                Next

            End With

            'スプレッドに鑑最低保証があるときだけ処理を行う
            If minAmount <> 0 Then
                'マスタの鑑最低保証額とスプレッド.鑑最低保証額+スプレッド.鑑最低保証対象の合計が異なる場合
                '（スプレッド内に鑑最低保証額が存在するときは合計値がマスタの値と同じ。ずれてる場合は別の取込をしている。）
                If minAmountMst <> minAmount + sumAmount Then
                    If (MyBase.ShowMessage(Me._Frm, "W280") = MsgBoxResult.Ok) Then
                        Dim max As Integer = Me._Frm.sprSeikyuM.ActiveSheet.Rows.Count - 1
                        For i As Integer = 0 To max
                            groupKb = Me._ControlV.GetCellValue(Me._Frm.sprSeikyuM.ActiveSheet.Cells(i, LMG050G.sprSeikyuMDef.GROUP_CD_KBN.ColNo))
                            sqKmkCd = Me._ControlV.GetCellValue(Me._Frm.sprSeikyuM.ActiveSheet.Cells(i, LMG050G.sprSeikyuMDef.SHUBETU_KBN.ColNo))

                            If LMGControlC.STORQAGE_FEE1.Equals(groupKb) _
                                AndAlso LMGControlC.SEIQKMK_GYOMUITAKU_JIMU.Equals(sqKmkCd) _
                                AndAlso LMGControlC.DETAIL_SAKUSEI_AUTO.Equals(autoKb) Then
                                '請求金額の再計算(0を下回るときは0にする)
                                Dim amt As Long = minAmountMst - sumAmount
                                If amt < 0 Then
                                    amt = 0
                                End If
                                Me._Frm.sprSeikyuM.SetCellValue(i, LMG050G.sprSeikyuMDef.KEISAN_GAKU.ColNo, amt)
                            End If
                        Next
                    End If
                End If
            End If
        End If

        Return True

    End Function

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================
#End Region

#End Region

End Class