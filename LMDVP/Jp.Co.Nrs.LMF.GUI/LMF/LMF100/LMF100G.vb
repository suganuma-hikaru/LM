' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送サブシステム
'  プログラムID     :  LMF100G : 帳票印刷指示
'  作  成  者       :  篠原
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Base
Imports GrapeCity.Win.Editors
'Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMF100Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMF100G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF100F

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMF100V

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconG As LMFControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF100F, ByRef g As LMFControlG, ByRef v As LMF100V)

        '親クラスのコンストラクタを呼ぶ。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付る。
        MyBase.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付ける。
        MyBase.MyForm = frm

        Me._Frm = frm

        'Gamen共通クラスの設定
        Me._LMFconG = g

        Me._V = v

    End Sub

#End Region

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True
        Dim lock As Boolean = False

        Dim empty As String = String.Empty

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = empty
            .F2ButtonName = empty
            .F3ButtonName = empty
            .F4ButtonName = empty
            .F5ButtonName = empty
            .F6ButtonName = empty
            .F7ButtonName = LMFControlC.FUNCTION_PRINT
            .F8ButtonName = empty
            .F9ButtonName = empty
            .F10ButtonName = LMFControlC.FUNCTION_POP
            .F11ButtonName = empty
            .F12ButtonName = LMFControlC.FUNCTION_CLOSE

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

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .cmbPrint.TabIndex = LMF100C.CtlTabIndex.Print
            .cmbBr.TabIndex = LMF100C.CtlTabIndex.Br
            .txtCustCdL.TabIndex = LMF100C.CtlTabIndex.CustCdL
            .lblCustNmL.TabIndex = LMF100C.CtlTabIndex.CustNmL
            .txtCustCdM.TabIndex = LMF100C.CtlTabIndex.CustCdM
            .lblCustNmM.TabIndex = LMF100C.CtlTabIndex.CustNmM
            .cmbKen.TabIndex = LMF100C.CtlTabIndex.CmbKen
            .imdOutkaDateFrom.TabIndex = LMF100C.CtlTabIndex.OutkaDateFrom
            .imdOutkaDateTo.TabIndex = LMF100C.CtlTabIndex.OutkaDateTo

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal data As String, ByVal ds As DataSet)

        '編集部の項目をクリア
        Call Me.ClearControl()

        '自営業所の設定、当日日付の設定
        Call Me.SetInput(data)

        'コンボの設定
        Call Me.SetValue(ds)


    End Sub


    ''' <summary>
    ''' 新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetValue(ByVal ds As DataSet)

        With Me._Frm.cmbKen

            'リストのクリア 
            .Items.Clear()

            Dim KEN_NM As String = String.Empty
            Dim KEN_CD As String = String.Empty

            '空行の設定
            .Items.Add(New ListItem(New SubItem() {New SubItem(KEN_CD), New SubItem(KEN_CD)}))

            'マスタ検索処理
            Dim dt As DataTable = ds.Tables(LMF100C.TABLE_NM_LMF100_KEN)
            Dim max As Integer = dt.Rows.Count - 1
            For i As Integer = 0 To max

                KEN_NM = dt.Rows(i).Item("KEN_NM").ToString()
                KEN_CD = dt.Rows(i).Item("KEN_CD").ToString() '★
                .Items.Add(New ListItem(New SubItem() {New SubItem(KEN_NM), New SubItem(KEN_CD)})) '★
            Next

        End With

    End Sub


#Region "コントロールの初期設定"
    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControl()

        With Me._Frm

            .cmbPrint.SelectedValue = Nothing
            .cmbBr.SelectedValue = Nothing
            .txtCustCdL.TextValue = String.Empty
            .lblCustNmL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNmM.TextValue = String.Empty
            .cmbKen.SelectedValue = Nothing
            .imdOutkaDateFrom.TextValue = String.Empty
            .imdOutkaDateTo.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    '''営業所、当日日付の取得
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetInput(ByVal data As String)
        With Me._Frm
            '営業所コードの取得
            .cmbBr.SelectedValue = LMUserInfoManager.GetNrsBrCd()

            '当日日付の取得
            Dim nowDate As String = String.Concat(Convert.ToDateTime(DateFormatUtility.EditSlash(data)).ToString("yyyyMMdd"))

            '日付From
            .imdOutkaDateFrom.TextValue = nowDate

            '日付To
            .imdOutkaDateTo.TextValue = nowDate


        End With

    End Sub

    'START YANAI 要望番号582
    ''' <summary>
    '''荷主・日付の設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetPrmData(ByVal prmDs As DataSet)

        With Me._Frm
            If 0 < prmDs.Tables(LMF100C.TABLE_NM_IN_KEN_BETSU).Rows.Count Then
                .txtCustCdL.TextValue = prmDs.Tables(LMF100C.TABLE_NM_IN_KEN_BETSU).Rows(0).Item("CUST_CD_L").ToString()
                .txtCustCdM.TextValue = prmDs.Tables(LMF100C.TABLE_NM_IN_KEN_BETSU).Rows(0).Item("CUST_CD_M").ToString()
                If String.IsNullOrEmpty(prmDs.Tables(LMF100C.TABLE_NM_IN_KEN_BETSU).Rows(0).Item("F_DATE").ToString()) = False Then
                    .imdOutkaDateFrom.TextValue = prmDs.Tables(LMF100C.TABLE_NM_IN_KEN_BETSU).Rows(0).Item("F_DATE").ToString()
                End If
                If String.IsNullOrEmpty(prmDs.Tables(LMF100C.TABLE_NM_IN_KEN_BETSU).Rows(0).Item("T_DATE").ToString()) = False Then
                    .imdOutkaDateTo.TextValue = prmDs.Tables(LMF100C.TABLE_NM_IN_KEN_BETSU).Rows(0).Item("T_DATE").ToString()
                End If

                '名称を取得し、ラベルに表示を行う
                'Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                '                                                                                              "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND " _
                '                                                                                            , "CUST_CD_M = '", .txtCustCdM.TextValue, "'" _
                '                                                                               ))
                '20161003 要番2622 tsunehira add
                Dim dr As DataRow() = Me._LMFconG.SelectCustListDataRow(.cmbBr.SelectedValue.ToString(), .txtCustCdL.TextValue, .txtCustCdM.TextValue)

                If 0 < dr.Length Then
                    .lblCustNmL.TextValue = dr(0).Item("CUST_NM_L").ToString()
                    .lblCustNmM.TextValue = dr(0).Item("CUST_NM_M").ToString()
                End If

            End If
        End With

    End Sub
    'END YANAI 要望番号582

#End Region


    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()

    End Sub

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm
            .cmbPrint.Focus()
        End With
    End Sub


#End Region

#Region "印刷区分変更時"
    ''' <summary>
    ''' 印刷区分値変更のロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub Locktairff(ByVal frm As LMF100F)

        With Me._Frm

            Dim lockflgCust As Boolean = False
            Dim lockflgSiqto As Boolean = False
            Dim lockflgdata As Boolean = False

            '印刷区分
            Dim PrintKb As String = .cmbPrint.SelectedValue.ToString

            Me.SetLockControl(.cmbPrint, lockflgCust)
            Me.SetLockControl(.txtCustCdL, lockflgCust)
            Me.SetLockControl(.txtCustCdM, lockflgCust)
            Me.SetLockControl(.imdOutkaDateFrom, lockflgdata)
            Me.SetLockControl(.imdOutkaDateTo, lockflgdata)

        End With

    End Sub
#End Region


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

End Class
