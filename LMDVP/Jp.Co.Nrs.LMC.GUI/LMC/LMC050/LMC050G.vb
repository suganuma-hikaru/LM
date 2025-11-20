' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC050G : 出荷帳票印刷
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

Imports Jp.Co.Nrs.LM.Base


''' <summary>
''' LMC050Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC050G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMC050F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMC050F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

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
            .F7ButtonName = "印　刷"
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = always
            .F8ButtonEnabled = False
            .F9ButtonEnabled = False
            .F10ButtonEnabled = always
            .F11ButtonEnabled = False
            .F12ButtonEnabled = always

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

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

            'Main
            .cmbPrint.TabIndex = LMC050C.CtlTabIndex.Print
            .cmbEigyo.TabIndex = LMC050C.CtlTabIndex.Eigyo
            .txtCustCD_L.TabIndex = LMC050C.CtlTabIndex.CustCD_L
            .lblCustNM_L.TabIndex = LMC050C.CtlTabIndex.CustNM_L
            .txtCustCD_M.TabIndex = LMC050C.CtlTabIndex.CustCD_M
            .lblCustNM_M.TabIndex = LMC050C.CtlTabIndex.CustNM_M
            .imdSyukkaDate.TabIndex = LMC050C.CtlTabIndex.SyukkaDate
            .chkFurikae.TabIndex = LMC050C.CtlTabIndex.Furikae
            .imdDataInsDate.TabIndex = LMC050C.CtlTabIndex.DataInsDate
            .imdPrintDate_S.TabIndex = LMC050C.CtlTabIndex.PrintDate_S
            .imdPrintDate_E.TabIndex = LMC050C.CtlTabIndex.PrintDate_E
            .txtGoodsCd.TabIndex = LMC050C.CtlTabIndex.GoodsCd
            .lblGoodsNm.TabIndex = LMC050C.CtlTabIndex.GoodsNm
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
            .txtCustCD_S.TabIndex = LMC050C.CtlTabIndex.CustCD_S
            .lblCustNM_S.TabIndex = LMC050C.CtlTabIndex.CustNM_S
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByVal frm As LMC050F)

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コントロールに初期値設定
        Call Me.SetInitControl(id, frm)

    End Sub
    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LMC050F)

        '初期荷主情報取得
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST). _
        Select("SYS_DEL_FLG = '0' AND USER_CD = '" & _
               LM.Base.LMUserInfoManager.GetUserID() & "' AND DEFAULT_CUST_YN = '01'")

        '初期値が存在するコントロール
        frm.cmbEigyo.SelectedValue() = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()     '(自) 営業所

        '2014.08.04 FFEM高取対応 START
        'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
        Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

        If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
            frm.cmbEigyo.ReadOnly = True
        Else
            frm.cmbEigyo.ReadOnly = False
        End If

        If getDr.Length() > 0 Then
            frm.txtCustCD_L.TextValue = getDr(0).Item("CUST_CD_L").ToString()                '(初期) 荷主コード(大)
            frm.lblCustNM_L.TextValue = getDr(0).Item("CUST_NM_L").ToString()                '(初期) 荷主名(大)
            frm.txtCustCD_M.TextValue = getDr(0).Item("CUST_CD_M").ToString()                '(初期) 荷主コード(中)
            frm.lblCustNM_M.TextValue = getDr(0).Item("CUST_NM_M").ToString()                '(初期) 荷主名(中)
        End If

    End Sub
    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(Optional ByVal tmpKBN As String = "")

        With Me._Frm
            .cmbPrint.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbPrint.SelectedValue = String.Empty
            .cmbEigyo.SelectedValue = String.Empty
            .txtCustCD_L.TextValue = String.Empty
            .lblCustNM_L.TextValue = String.Empty
            .txtCustCD_M.TextValue = String.Empty
            .lblCustNM_M.TextValue = String.Empty
            .imdSyukkaDate.TextValue = String.Empty
            .imdDataInsDate.TextValue = String.Empty
            .imdPrintDate_S.TextValue = String.Empty
            .imdPrintDate_E.TextValue = String.Empty
            .txtGoodsCd.TextValue = String.Empty
            .lblGoodsNm.TextValue = String.Empty
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
            .txtCustCD_S.TextValue = String.Empty
            .lblCustNM_S.TextValue = String.Empty
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --
        End With

    End Sub

#Region "印刷区分変更時"
    ''' <summary>
    ''' 印刷区分値変更のロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub Locktairff(ByVal frm As LMC050F)

        With Me._Frm

            Dim lockflg1 As Boolean = False
            Dim lockflg2 As Boolean = False
            '(2012.11.06)MDB帳票化対応 -- START -- 
            Dim lockflg3 As Boolean = False
            '(2012.11.06)MDB帳票化対応 --  END  -- 

            '2013.07.31 追加START
            Dim lockflg4 As Boolean = False
            '2013.07.31 追加END

            '印刷区分
            Dim PrintKb As String = .cmbPrint.SelectedValue.ToString

            '(2012.02.28) 出荷報告書(月次)追加に伴い、If文からCase文に変更 --- START ---
            ''印刷区分が荷別出荷報告書の場合
            'If LMC050C.PRINT01.Equals(PrintKb) = False Then
            '    lockflg1 = True
            '    .imdDataInsDate.TextValue = String.Empty
            '    .imdSyukkaDate.TextValue = String.Empty
            'Else
            '    '印刷区分が出荷データチェックリスト
            '    lockflg2 = True
            '    .imdPrintDate_S.TextValue = String.Empty
            '    .imdPrintDate_E.TextValue = String.Empty
            '    .txtGoodsCd.TextValue = String.Empty
            '    .lblGoodsNm.TextValue = String.Empty
            'End If

            Select Case PrintKb
                Case LMC050C.PRINT02
                    '日別出荷報告書
                    lockflg1 = True
                    .imdDataInsDate.TextValue = String.Empty
                    .imdSyukkaDate.TextValue = String.Empty

                Case LMC050C.PRINT01, LMC050C.PRINT03
                    '出荷データチェックリスト、出荷報告書(月次)
                    lockflg2 = True
                    .imdPrintDate_S.TextValue = String.Empty
                    .imdPrintDate_E.TextValue = String.Empty
                    .txtGoodsCd.TextValue = String.Empty
                    .lblGoodsNm.TextValue = String.Empty
                    '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
                    .txtCustCD_S.TextValue = String.Empty
                    .lblCustNM_S.TextValue = String.Empty
                    '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --

                    '(2012.11.06)MDB帳票化対応 -- START -- 
                Case LMC050C.PRINT04, LMC050C.PRINT05
                    'JAL改定、ANA改定
                    lockflg3 = True
                    .imdPrintDate_S.TextValue = String.Empty
                    .imdPrintDate_E.TextValue = String.Empty
                    .txtGoodsCd.TextValue = String.Empty
                    .lblGoodsNm.TextValue = String.Empty
                    .imdDataInsDate.TextValue = String.Empty
                    .chkFurikae.Checked = False
                    '(2012.11.06)MDB帳票化対応 --  END  --
                    '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
                    .txtCustCD_S.TextValue = String.Empty
                    .lblCustNM_S.TextValue = String.Empty
                    '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --

                    '2013.07.31 追加START
                Case LMC050C.PRINT06
                    'BYK出荷報告作成
                    lockflg4 = True
                    .imdPrintDate_S.TextValue = String.Empty
                    .imdPrintDate_E.TextValue = String.Empty
                    .txtGoodsCd.TextValue = String.Empty
                    .lblGoodsNm.TextValue = String.Empty
                    .imdDataInsDate.TextValue = String.Empty
                    .chkFurikae.Checked = False
                    .txtCustCD_S.TextValue = String.Empty
                    .lblCustNM_S.TextValue = String.Empty
                    '2013.07.31 追加END

            End Select
            '(2012.02.28) 出荷報告書(月次)追加に伴い、If文からCase文に変更 ---  END  ---

            '印刷分類が空欄の場合
            If String.IsNullOrEmpty(PrintKb) = True Then
                '全てロック解除
                lockflg1 = False
                lockflg2 = False
            End If

            Me.SetLockControl(.imdDataInsDate, lockflg1)
            Me.SetLockControl(.imdSyukkaDate, lockflg1)
            Me.SetLockControl(.imdPrintDate_S, lockflg2)
            Me.SetLockControl(.imdPrintDate_E, lockflg2)
            Me.SetLockControl(.txtGoodsCd, lockflg2)
            '(2013.01.21)MDB帳票化対応 -- START --
            Me.SetLockControl(.txtCustCD_S, lockflg2)
            '(2013.01.21)MDB帳票化対応 --  EnD  --

            '(2012.11.06)MDB帳票化対応 -- START --
            'JAL改定、ANA改定
            If PrintKb = LMC050C.PRINT04 Or PrintKb = LMC050C.PRINT05 Then
                Me.SetLockControl(.imdDataInsDate, lockflg3)
                Me.SetLockControl(.imdPrintDate_S, lockflg3)
                Me.SetLockControl(.imdPrintDate_E, lockflg3)
                Me.SetLockControl(.txtGoodsCd, lockflg3)
                Me.SetLockControl(.imdDataInsDate, lockflg3)
                Me.SetLockControl(.chkFurikae, lockflg3)
            End If
            '(2012.11.06)MDB帳票化対応 --  END  --

            '2013.07.31 追加START
            If PrintKb = LMC050C.PRINT06 Then
                Me.SetLockControl(.imdDataInsDate, lockflg4)
                Me.SetLockControl(.imdPrintDate_S, lockflg4)
                Me.SetLockControl(.imdPrintDate_E, lockflg4)
                Me.SetLockControl(.txtGoodsCd, lockflg4)
                Me.SetLockControl(.imdDataInsDate, lockflg4)
                Me.SetLockControl(.chkFurikae, lockflg4)
                Me.SetLockControl(.txtCustCD_S, lockflg4)
            End If
            '2013.07.31 追加END

        End With

    End Sub
#End Region

#Region "部品化検討中"
    ''' <summary>
    ''' ロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="ctl">制御対象項目</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Private Sub SetLockControl(ByVal ctl As Control, Optional ByVal lockFlg As Boolean = False)

        Dim arr As ArrayList = New ArrayList()
        Call Me.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
        Dim lblArr As ArrayList = New ArrayList()

        'エディット系コントロールのロック
        For Each arrCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In arr

            'テキストボックスの場合、ラベル項目であったら処理対象外とする
            If TypeOf arrCtl Is Win.InputMan.LMImTextBox = True Then

                If DirectCast(arrCtl, Win.InputMan.LMImTextBox).Name.Substring(0, 3).Equals("lbl") = True Then
                    lblArr.Add(arrCtl)
                End If

            End If

            'ロック処理/ロック解除処理を行う
            arrCtl.ReadOnlyStatus = lockFlg
        Next

        'ラベル項目をロック
        For Each lblCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In lblArr
            lblCtl.ReadOnlyStatus = True
        Next

        'ボタンのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMButton)(arr, ctl)
        For Each arrCtl As Win.LMButton In arr
            'ロック処理/ロック解除処理を行う
            Call Me.LockButton(arrCtl, lockFlg)
        Next

        'チェックボックスのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMCheckBox)(arr, ctl)
        For Each arrCtl As Win.LMCheckBox In arr

            'ロック処理/ロック解除処理を行う
            Call Me.LockCheckBox(arrCtl, lockFlg)

        Next

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(ボタン)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockButton(ByVal ctl As Win.LMButton, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.Enabled = enabledFlg

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(チェックボックス)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockCheckBox(ByVal ctl As Win.LMCheckBox, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.EnableStatus = enabledFlg

    End Sub

    ''' <summary>
    ''' 画面上のコントロールから指定した型のクラスかその継承クラスを取得する
    ''' </summary>
    ''' <param name="arr">取得したコントロールを格納するArrayList</param>
    ''' <param name="ownControl">コントロール取得元となるコントロール</param>
    ''' <remarks></remarks>
    Private Sub GetTarget(Of T)(ByVal arr As ArrayList, ByVal ownControl As Control)

        If TypeOf ownControl Is T _
            OrElse ownControl.GetType.IsSubclassOf(GetType(T)) Then

            '指定されたクラスかその継承クラス
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub


#End Region

#End Region

#End Region

#End Region

End Class
