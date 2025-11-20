' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI470  : 日本合成　物流費送信
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI470ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI470H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI470V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI470G

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
        Dim frm As LMI470F = New LMI470F(Me)

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
        Me._G = New LMI470G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMI470V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        ''営業所,倉庫コンボ関連設定
        'MyBase.CreateSokoCombData(frm.cmbEigyo, Nothing)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G006")

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

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMI470C.EventShubetsu, ByVal frm As LMI470F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMI470C.EventShubetsu.JIKKO    '実行

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '物流費作成処理
                '①税抜き金額チェック
                If Me.Butsuryuhi_Chk(frm) = True Then
                    '②物流費作成
                    Call Me.Butsuryuhi_Sakusei(frm)
                End If

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

            Case LMI470C.EventShubetsu.MASTER    'マスタ参照

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '現在フォーカスのあるコントロール名の取得
                Dim objNm As String = frm.FocusedControlName()

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '請求先コード
                Call Me.ShowPopup(frm, objNm, prm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI470F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByVal frm As LMI470F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey5Press")

        '「実行」処理
        Me.ActionControl(LMI470C.EventShubetsu.JIKKO, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey5Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMI470F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True

        End If

        Me.ActionControl(LMI470C.EventShubetsu.MASTER, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI470F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LMI470F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================


    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"


    ''' <summary>
    ''' マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>参照Popupが開く場合のコントロールです。</remarks>
    Private Sub ShowPopup(ByVal frm As LMI470F, ByVal objNM As String, ByRef prm As LMFormData)

        'オブジェクト名による分岐
        Select Case objNM

            Case "txtCustCdL", "txtCustCdM" '請求先マスタ参照

                Dim prmDs As DataSet = New LMZ220DS
                Dim row As DataRow = prmDs.Tables(LMZ220C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
                If Me._PopupSkipFlg = False Then
                    row("CUST_CD_L") = frm.txtSeikyuCd.TextValue
                End If
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row("HYOJI_KBN") = LMZControlC.HYOJI_SS
                prmDs.Tables(LMZ220C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                frm.lblSeikyuNm.TextValue = String.Empty

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ220", prm)

        End Select

        '戻り処理
        If prm.ReturnFlg = True Then
            Select Case objNM

                Case "txtSeikyuCd" '請求先マスタ参照

                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)
                        '戻り値の設定
                        frm.txtSeikyuCd.TextValue = .Item("SEIQTO_CD").ToString()
                        frm.lblSeikyuNm.TextValue = .Item("SEIQTO_NM").ToString()
                    End With

            End Select

        End If

        MyBase.ShowMessage(frm, "G006")

    End Sub

    ''' <summary>
    ''' 物流費データチェック処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function Butsuryuhi_Chk(ByVal frm As LMI470F) As Boolean

        Butsuryuhi_Chk = False

        'DataSet設定
        Dim rtDs As DataSet = New LMI470DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)
        rtDs.Tables("LMI470IN").Rows(0).Item("SYORI_PTN") = "1".ToString        '金額チェック


        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Butsuryuhi_Chk")

        Dim inpNetAMt As Integer = CInt(rtDs.Tables("LMI470IN").Rows(0).Item("KINGAKU").ToString)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA( _
                                             "LMI470BLF", _
                                             "Butsuryuhi_Rtn", _
                                             rtDs)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Function
        End If

        Dim G_Net_Amt As Integer = 0

        For i As Integer = 0 To rtnDs.Tables("LMI470OUT").Rows.Count - 1

            G_Net_Amt += CInt(rtnDs.Tables("LMI470OUT").Rows(i).Item("NET_AMT").ToString)
        Next

        '抽出結果をセット
        frm.numKingakuSQL.Value = G_Net_Amt

        If G_Net_Amt.Equals(inpNetAMt) = False Then
            MyBase.ShowMessage(frm, "E028", New String() {"入力金額とデータ抽出金額が不一致", "物流費データ作成"})
            frm.numKingaku.Focus()

            Exit Function
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "Butsuryuhi_Chk")

        Return True

    End Function

    ''' <summary>
    ''' 物流費データ作成処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub Butsuryuhi_Sakusei(ByVal frm As LMI470F)

        'DataSet設定
        Dim rtDs As DataSet = New LMI470DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)
        rtDs.Tables("LMI470IN").Rows(0).Item("SYORI_PTN") = "2".ToString        '物流費作成

        ' キャッシュテーブル区分Ｍ取得（セットするフォルダー、ファイル名取得）
        Dim drs As DataRow() = Me.SelectKbnListDataRow("01", "F023")
        If 0 < drs.Length Then
            'システム日付の取得
            Dim sysdate As String() = MyBase.GetSystemDateTime()

            rtDs.Tables("LMI470IN").Rows(0).Item("FOLDER_NM") = drs(0).Item("KBN_NM1").ToString.Trim
#If False Then      'UPD 2018/08/07 先頭システム日付を取る
            rtDs.Tables("LMI470IN").Rows(0).Item("FILE_NM") = String.Concat(sysdate(0).ToString.Trim, drs(0).Item("KBN_NM2").ToString.Trim) 

#Else
            rtDs.Tables("LMI470IN").Rows(0).Item("FILE_NM") = drs(0).Item("KBN_NM2").ToString.Trim

#End If
        End If


        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Butsuryuhi_Soushin")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA( _
                                             "LMI470BLF", _
                                             "Butsuryuhi_Rtn", _
                                             rtDs)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"物流費作成処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "Butsuryuhi_Soushin")

    End Sub


#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInData(ByVal frm As LMI470F, ByRef rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMI470C.TABLE_NM_IN).NewRow()

        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("SEIQTO_CD") = frm.txtSeikyuCd.TextValue
        dr("DATE_FROM") = frm.imdOutkaDateFrom.TextValue
        dr("DATE_TO") = frm.imdOutkaDateTo.TextValue
        dr("KINGAKU") = frm.numKingaku.TextValue


        'データセットの追加
        rtDs.Tables(LMI470C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

#End Region

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード</param>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnListDataRow(ByVal kbnCd As String _
                                         , ByVal groupCd As String _
                                         ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(Me.SelectKbnString(kbnCd, groupCd))

    End Function


    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード</param>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnString(ByVal kbnCd As String _
                                     , ByVal groupCd As String _
                                     ) As String

        SelectKbnString = String.Empty

        '削除フラグ
        SelectKbnString = String.Concat(SelectKbnString, " SYS_DEL_FLG = '0' ")

        '区分コード
        SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_CD = ", " '", kbnCd, "' ")

        '区分グループコード
        SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_GROUP_CD = ", " '", groupCd, "' ")

        Return SelectKbnString

    End Function
#End Region 'Method

End Class
