' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB030G : 入荷報告書
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
''' LMB030Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB030G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB030F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB030F)

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
    Friend Sub SetFunctionKey(ByVal mode As String)

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
            .cmbPrint.TabIndex = LMB030C.CtlTabIndex.Print
            .cmbEigyo.TabIndex = LMB030C.CtlTabIndex.Eigyo
            .txtCustCD_L.TabIndex = LMB030C.CtlTabIndex.CustCDL
            .lblCustNM_L.TabIndex = LMB030C.CtlTabIndex.CustNML
            .txtCustCD_M.TabIndex = LMB030C.CtlTabIndex.CustCDM
            .lblCustNM_M.TabIndex = LMB030C.CtlTabIndex.CustNMM
            .imdNyukaDate.TabIndex = LMB030C.CtlTabIndex.NyukaDate
            .chkFurikae.TabIndex = LMB030C.CtlTabIndex.Furikae
            .imdDataInsDate.TabIndex = LMB030C.CtlTabIndex.DataInsDate

        End With



    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="lockFlg">モードによるロック制御を行う。省略時：初期モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal lockFlg As String = LMB030C.MODE_DEFAULT)

        Dim noMnb As Boolean = True
        Dim dtTori As Boolean = True

        With Me._Frm


        End With

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
            .imdNyukaDate.TextValue = String.Empty
            .imdDataInsDate.TextValue = String.Empty
           

        End With

    End Sub

    ''' <summary>
    '''営業所コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetcmbNrsBrCd(ByVal frm As LMB030F)

        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        Me._Frm.cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()
        'Me._Frm.cmbEigyo.SelectedValue = frm.cmbEigyo.SelectedValue.ToString()

        '2014.08.04 FFEM高取対応 START
        'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
        Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

        If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
            frm.cmbEigyo.ReadOnly = True
        Else
            frm.cmbEigyo.ReadOnly = False
        End If
    End Sub
#Region "印刷ボタン変更時"
    ''' <summary>
    ''' タリフ分類区分値変更のロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub Locktairff(ByVal frm As LMB030F)

        With Me._Frm

            Dim lockflg1 As Boolean = False


            '印刷区分
            Dim PrintKb As String = .cmbPrint.SelectedValue.ToString

            '印刷区分が月次時
            'データ登録日をロック
            If LMB030C.GETUJI.Equals(PrintKb) = True Then

                lockflg1 = True
                .imdDataInsDate.TextValue = String.Empty

            End If


            '印刷分類が空欄の場合
            If String.IsNullOrEmpty(PrintKb) = True Then
                '全てロック解除
                lockflg1 = False


            End If

            Call Me.SetLockControl(.imdDataInsDate, lockflg1)

        End With


    End Sub
#End Region

#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataSet)

        '参考値の設定
        ' Call Me.SetSpread(ds)

        'Dim getDt As DataTable = ds.Tables(LMB030C.TABLE_NM_OUT)

        'Me._Frm.txtMemno.TextValue = getDt.Rows(0).Item("KMMID").ToString

        'For index As Integer = 0 To (getDt.Rows().Count - 1)

        '    Dim strKNAPTA As String = getDt.Rows(index).Item("KNAPTA").ToString

        '    Select Case Mid(strKNAPTA, 1, 1)

        '        Case "1"
        '            '取得データをヘッダ部に表示
        '            Call SetHeaderData(strKNAPTA, "1")
        '        Case "2"
        '            '取得データをSpreadに表示
        '            Call Me.SetSpread(strKNAPTA)
        '        Case "8"
        '            '取得データをヘッダ部に表示
        '            Call SetHeaderData(strKNAPTA, "8")

        '    End Select
        'Next

    End Sub

    ''' <summary>
    ''' 検索結果ヘッダー部表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHeaderData(ByVal strKNAPTA As String, ByVal strKBN As String)

    End Sub

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
