' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI050  : EDI月末在庫実績送信ﾃﾞｰﾀ作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports GrapeCity.Win.Editors

''' <summary>
''' LMI050Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI050G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI050F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI050F, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMIConG = g

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
        Dim edit As Boolean = False
        Dim view As Boolean = False
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = LMIControlC.FUNCTION_JIKKO
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = always
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = lock
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region 'FunctionKey

#Region "Mode&Status"

    ''' <summary>
    ''' Dispモードとレコードステータスの設定
    ''' </summary>
    ''' <param name="mode">Dispモード</param>
    ''' <param name="status">レコードステータス</param>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(ByVal mode As String, ByVal status As String)

        With Me._Frm

        End With

    End Sub

#End Region 'Mode&Status

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .grpSearch.TabIndex = LMI050C.CtlTabIndex.GRPSERCH
            .cmbEigyo.TabIndex = LMI050C.CtlTabIndex.EIGYO
            .cmbCust.TabIndex = LMI050C.CtlTabIndex.CUST
            .imdDate.TabIndex = LMI050C.CtlTabIndex.JIKKODATE
            .chkEDI.TabIndex = LMI050C.CtlTabIndex.CHKEDI
            .chkExcel.TabIndex = LMI050C.CtlTabIndex.CHKEXCEL
            .chkMail.TabIndex = LMI050C.CtlTabIndex.CHKMAIL
            .grpZaiko.TabIndex = LMI050C.CtlTabIndex.GRPZAIKO
            .optZaiko.TabIndex = LMI050C.CtlTabIndex.OPTZAIKO
            .optZaizan.TabIndex = LMI050C.CtlTabIndex.OPTZAIZAN

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String)

        With Me._Frm

            '自営業所の設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbEigyo.SelectedValue = brCd

            '荷主コンボの作成
            Me.CreateCustCombo()

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .cmbCust.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' 荷主単位の画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlCust(ByVal sysDate As String)

        With Me._Frm
            '各コントロールの初期化
            .imdDate.TextValue = String.Empty
            .chkEDI.Checked = False
            .chkEDI.Enabled = True
            .chkExcel.Checked = False
            .chkExcel.Enabled = True
            .chkMail.Checked = False
            .chkMail.Enabled = True
            .optZaiko.Checked = False
            .optZaiko.Enabled = True
            .optZaizan.Checked = False
            .optZaizan.Enabled = True

            '区分マスタを取得し、区分マスタに設定されている値で設定する
            Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'E029' AND ", _
                                                                                                            "KBN_CD = '", .cmbCust.SelectedValue, "'"))
            If kbnDr.Length > 0 Then
                '実績日付の設定
                Dim sysDay As String = Mid(sysDate, 7, 2) 'システム日だけを保持
                '今月の末日を求める
                Dim lastDate As String = Convert.ToString(DateSerial(Convert.ToInt32(Mid(sysDate, 1, 4)), Convert.ToInt32(Mid(sysDate, 5, 2)), 1).AddMonths(1).AddDays(-1))
                lastDate = String.Concat(Mid(lastDate, 1, 4), Mid(lastDate, 6, 2), Mid(lastDate, 9, 2))
                '前月の末日を求める
                Dim lastDateOld As String = Convert.ToString(DateSerial(Convert.ToInt32(Mid(sysDate, 1, 4)), Convert.ToInt32(Mid(sysDate, 5, 2)), 1).AddDays(-1))
                lastDateOld = String.Concat(Mid(lastDateOld, 1, 4), Mid(lastDateOld, 6, 2), Mid(lastDateOld, 9, 2))

                Select Case kbnDr(0).Item("KBN_NM7").ToString
                    Case LMI050C.DATE_FLG98
                        'システム日付を設定
                        .imdDate.TextValue = sysDate

                    Case LMI050C.DATE_FLG00, LMI050C.DATE_FLG99
                        If Convert.ToInt32(sysDay) > 20 Then
                            '今月の末日を設定
                            .imdDate.TextValue = lastDate
                        Else
                            '先月の末日を設定
                            .imdDate.TextValue = lastDateOld
                        End If

                    Case Else
                        If Convert.ToInt32(sysDay) > Convert.ToInt32(kbnDr(0).Item("KBN_NM7").ToString) - 10 Then
                            '今月の日の部分をKBN_NM7の値に変えた日を設定
                            .imdDate.TextValue = String.Concat(Mid(lastDate, 1, 6), kbnDr(0).Item("KBN_NM7").ToString)
                        Else
                            '前月の日の部分をKBN_NM7の値に変えた日を設定
                            .imdDate.TextValue = String.Concat(Mid(lastDateOld, 1, 6), kbnDr(0).Item("KBN_NM7").ToString)
                        End If

                End Select

                'EDI送信用チェックボックスの設定
                .chkEDI.Enabled = False
                If ("01").Equals(kbnDr(0).Item("KBN_NM4").ToString) = True Then
                    .chkEDI.Checked = True
                    .chkEDI.Enabled = True
                End If

                'EXCEL送信用チェックボックスの設定
                .chkExcel.Enabled = False
                If ("01").Equals(kbnDr(0).Item("KBN_NM5").ToString) = True Then
                    .chkExcel.Checked = True
                    .chkExcel.Enabled = True
                End If

                'メール送信用チェックボックスの設定
                .chkMail.Enabled = False
                If ("01").Equals(kbnDr(0).Item("KBN_NM6").ToString) = True Then
                    .chkMail.Checked = True
                    .chkMail.Enabled = True
                End If

                '作成元在庫部の設定
                .optZaiko.Enabled = False
                .optZaizan.Enabled = False
                If ("01").Equals(kbnDr(0).Item("KBN_NM8").ToString) = True Then
                    .optZaiko.Enabled = True
                    .optZaizan.Enabled = True
                End If

            End If

        End With

    End Sub

    ''' <summary>
    ''' 荷主コンボの作成
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateCustCombo()

        With Me._Frm

            '荷主コンボの作成
            .cmbCust.SelectedValue() = ""
            .cmbCust.Items.Clear()
            Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'E029' AND ", _
                                                                                                            "KBN_NM1 = '", .cmbEigyo.SelectedValue, "'"))
            Dim custDr() As DataRow = Nothing
            Dim max As Integer = kbnDr.Length - 1
            For i As Integer = 0 To max
                custDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = '", kbnDr(i).Item("KBN_NM2").ToString, "' AND ", _
                                                                                                 "CUST_CD_M = '", kbnDr(i).Item("KBN_NM3").ToString, "'"))
                If custDr.Length > 0 Then
                    .cmbCust.Items.Add(New ListItem(New SubItem() {New SubItem( _
                                                                                String.Concat(custDr(0).Item("CUST_NM_L").ToString, "　", custDr(0).Item("CUST_NM_M").ToString)), _
                                                                                New SubItem(kbnDr(i).Item("KBN_CD").ToString)}))
                End If
            Next

        End With

    End Sub

#End Region

#Region "Spread"

#End Region

#Region "ユーティリティ"

#Region "プロパティ"

#End Region

#End Region

#End Region

End Class
