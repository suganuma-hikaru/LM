' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ     : ｼｽﾃﾑ管理
'  プログラムID     :  LMJ010G : 請求在庫・実在庫差異分リスト作成
'  作  成  者       :  Shinohara
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李
Imports Jp.Co.Nrs.Win.Base   '2017/09/25 追加 李

''' <summary>
''' LMJ010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMJ010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMJ010F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMJconG As LMJControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMJ010F, ByVal g As LMJControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMJconG = g

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = LMJControlC.FUNCTION_CREATE
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = LMJControlC.FUNCTION_POP
            .F11ButtonName = String.Empty
            .F12ButtonName = LMJControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = always
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = always
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .cmbPrint.TabIndex = LMJ010C.CtlTabIndex.PRINT
            .grpSearch.TabIndex = LMJ010C.CtlTabIndex.SEARCH
            .cmbShori.TabIndex = LMJ010C.CtlTabIndex.SHORI
            .cmbEigyo.TabIndex = LMJ010C.CtlTabIndex.EIGYO
            .txtCustCdL.TabIndex = LMJ010C.CtlTabIndex.CUSTCDL
            .lblCustNmL.TabIndex = LMJ010C.CtlTabIndex.CUSTNML
            .txtCustCdM.TabIndex = LMJ010C.CtlTabIndex.CUSTCDM
            .lblCustNmM.TabIndex = LMJ010C.CtlTabIndex.CUSTNMM
            .imdSeiqDate.TabIndex = LMJ010C.CtlTabIndex.SEIQDATE
            .cmbSeiqComb.TabIndex = LMJ010C.CtlTabIndex.SEIQCOMB
            .cmbZaiko.TabIndex = LMJ010C.CtlTabIndex.ZAIKO
            .pnlSerial.TabIndex = LMJ010C.CtlTabIndex.SERIAL
            .optSerialAri.TabIndex = LMJ010C.CtlTabIndex.SERIALARI
            .optSerialNashi.TabIndex = LMJ010C.CtlTabIndex.SERIALNASHI

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        '日付コントロールの書式設定
        Call Me._LMJconG.SetDateFormat(Me._Frm.imdSeiqDate)

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(ByVal ds As DataSet, ByVal nowDate As String)

        With Me._Frm

            Dim lock As Boolean = True
            Dim seiqDateFlg1 As Boolean = True
            Dim seiqDateFlg2 As Boolean = True
            Dim visibleFlg As Boolean = False

            '請求日付の値初期化
            .cmbSeiqComb.SelectedValue = Nothing
            .cmbSeiqComb.TextValue = String.Empty

            '指定荷主のみ
            Dim shori As String = .cmbShori.SelectedValue.ToString()
            Select Case shori

                Case LMJ010C.SHORI_SONOTA

                    lock = False
                    visibleFlg = True
                    seiqDateFlg1 = False

                    '荷主の設定
                    Call Me.SetInitCustData()

                    '月末在庫コンボの設定
                    Call Me.SetZaikoDateControl(ds)

                    '請求日付の値設定
                    .imdSeiqDate.TextValue = Me.GetMatsuDate(nowDate)

                Case String.Empty
                Case Else
                    seiqDateFlg2 = False

            End Select

            'ロック制御
            Me._LMJconG.SetLockInputMan(.txtCustCdL, lock)
            Me._LMJconG.SetLockInputMan(.txtCustCdM, lock)
            Me._LMJconG.SetLockInputMan(.cmbZaiko, lock)
            Me._LMJconG.SetLockInputMan(.imdSeiqDate, seiqDateFlg1)
            Me._LMJconG.SetLockInputMan(.cmbSeiqComb, seiqDateFlg2)

            '荷主コードをロックする場合、名称をクリア
            If lock = True Then
                .lblCustNmL.TextValue = String.Empty
                .lblCustNmM.TextValue = String.Empty
            End If

            '表示の切り替え
            .imdSeiqDate.Visible = visibleFlg
            .cmbSeiqComb.Visible = Not visibleFlg

            '月末在庫(全荷主の場合)設定
            If lock = True Then

                '月末在庫コンボの設定
                Call Me.SetZaikoDateControl(nowDate)

            End If

        End With

    End Sub

    ''' <summary>
    ''' 末日を取得
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMatsuDate(ByVal value As String) As String

        Dim yyyyMM As String = value.Substring(0, 6)

        'まず当月の01日をDate型に変換
        Dim aDate As Date = Convert.ToDateTime(DateFormatUtility.EditSlash(String.Concat(yyyyMM, "01")))

        '-1日をyyyyMMdd形式に変換
        Return aDate.AddDays(-1).ToString(LMJControlC.DATE_YYYYMMDD)

    End Function

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            'フォーカス位置の初期化
            .Focus()

            .cmbPrint.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbPrint.SelectedValue = Nothing
            .cmbShori.SelectedValue = Nothing
            .cmbEigyo.SelectedValue = Nothing
            .txtCustCdL.TextValue = String.Empty
            .lblCustNmL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNmM.TextValue = String.Empty
            .imdSeiqDate.Value = Nothing
            .cmbSeiqComb.SelectedValue = Nothing
            .cmbZaiko.SelectedValue = Nothing

        End With

    End Sub

    ''' <summary>
    ''' 初期値の設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        With Me._Frm

            '種別の設定
            .cmbPrint.SelectedValue = LMJControlC.FLG_ON

            '自営業を設定
            .cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()

            '荷主の初期表示
            Call Me.SetInitCustData()

            '処理内容の設定
            .cmbShori.SelectedValue = LMJ010C.SHORI_SONOTA

            '無しを選択
            .optSerialNashi.Checked = True

        End With

    End Sub

    ''' <summary>
    ''' 荷主の初期表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInitCustData()

        With Me._Frm

            '初期荷主から値取得
            Dim drs As DataRow() = Me._LMJconG.SelectTCustListDataRow(LMUserInfoManager.GetUserID())
            If 0 < drs.Length Then

                Dim custCdL As String = drs(0).Item("CUST_CD_L").ToString()
                Dim custCdM As String = drs(0).Item("CUST_CD_M").ToString()
                .txtCustCdL.TextValue = custCdL
                .txtCustCdM.TextValue = custCdM
                drs = Me._LMJconG.SelectCustListDataRow(custCdL, custCdM, LMJControlC.FLG_OFF, LMJControlC.FLG_OFF)
                If 0 < drs.Length Then
                    .lblCustNmL.TextValue = drs(0).Item("CUST_NM_L").ToString()
                    .lblCustNmM.TextValue = drs(0).Item("CUST_NM_M").ToString()
                End If

            End If

        End With

    End Sub

    ''' <summary>
    ''' 請求日の初期設定
    ''' </summary>
    ''' <param name="nowDate">サーバ日付</param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal nowDate As String)

        Me._Frm.imdSeiqDate.TextValue = Me.GetMatsuDate(nowDate)

    End Sub

    ''' <summary>
    ''' 月末在庫コンボ生成処理(サーバ日付から作成)
    ''' </summary>
    ''' <param name="nowDate">サーバ日付</param>
    ''' <remarks></remarks>
    Private Sub SetZaikoDateControl(ByVal nowDate As String)

        With Me._Frm

            'リストをクリア
            .cmbZaiko.Items.Clear()
            .cmbSeiqComb.Items.Clear()

            Dim cd As String = String.Empty
            Dim item As String = String.Empty

            '空行追加
            Call Me._LMJconG.ComboBoxItemAdd(.cmbZaiko, cd, item)
            Call Me._LMJconG.ComboBoxItemAdd(.cmbSeiqComb, cd, item)

            '処理内容が空の場合、スルー
            Dim shori As String = .cmbShori.SelectedValue.ToString()
            If String.IsNullOrEmpty(shori) = True Then
                Exit Sub
            End If

            Dim setData As String = String.Empty
            Select Case shori

                Case LMJ010C.SHORI_MATSU

                    'サーバ日付の先月の末日
                    setData = Me.GetMatsuDate(nowDate)

                Case Else

                    '区分値 < サーバ日付の日にち
                    If shori < nowDate.Substring(6, 2) Then

                        '当年月 + 区分値
                        setData = String.Concat(nowDate.Substring(0, 6), shori)

                    Else

                        '先年月 + 区分値
                        setData = String.Concat(Me.GetMatsuDate(nowDate).Substring(0, 6), shori)

                    End If

            End Select

            Dim aDate As Date = Convert.ToDateTime(DateFormatUtility.EditSlash(setData))
            Dim arr As ArrayList = New ArrayList()
            Dim cnt As Integer = 1
            While cnt <= LMJ010C.LIST_CNT

                arr.Add(aDate.AddMonths(1 - cnt).ToString(LMJControlC.DATE_YYYYMMDD))
                cnt += 1

            End While

            Dim max As Integer = arr.Count - 1
            For i As Integer = 0 To max

                'リストの追加
                Me.ComboBoxItemAdd(.cmbZaiko, arr(i).ToString())
                Me.ComboBoxItemAdd(.cmbSeiqComb, arr(i).ToString())

            Next

            '追加したものの1番目を表示
            .cmbZaiko.SelectedIndex = 1
            .cmbSeiqComb.SelectedIndex = 1

        End With

    End Sub

    ''' <summary>
    ''' 月末在庫コンボ生成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetZaikoDateControl(ByVal ds As DataSet)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            Dim setDt As DataTable = ds.Tables(LMJ010C.TABLE_NM_GETU_OUT)

            'リストをクリア
            .cmbZaiko.Items.Clear()

            Dim cd As String = String.Empty
            Dim item As String = String.Empty

            '空行追加
            Call Me._LMJconG.ComboBoxItemAdd(.cmbZaiko, cd, item)

            '取得した情報の設定
            Dim max As Integer = setDt.Rows.Count - 1
            For i As Integer = 0 To max
                Call Me.ComboBoxItemAdd(.cmbZaiko, setDt(i).Item("RIREKI_DATE").ToString())
            Next

            '直近在庫の設定
            Dim drs As DataRow() = Me._LMJconG.SelectKbnListDataRow(LMJ010C.START_DATE, LMKbnConst.KBN_G003)

            '初期在庫の設定
            '2017/09/25 修正 李↓
            Me._LMJconG.ComboBoxItemAdd(.cmbZaiko, drs(0).Item("KBN_NM2").ToString(), drs(0).Item(lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})).ToString())
            '2017/09/25 修正 李↑

            '1行目を初期表示
            .cmbZaiko.SelectedIndex = 1

        End With

    End Sub

    ''' <summary>
    ''' コンボに行を追加　
    ''' </summary>
    ''' <param name="cmb">コントロール</param>
    ''' <param name="value">設定値</param>
    ''' <remarks></remarks>
    Private Sub ComboBoxItemAdd(ByVal cmb As LMImCombo, ByVal value As String)
        Me._LMJconG.ComboBoxItemAdd(cmb, value, DateFormatUtility.EditSlash(value))
    End Sub

#End Region

#End Region

#End Region

End Class
