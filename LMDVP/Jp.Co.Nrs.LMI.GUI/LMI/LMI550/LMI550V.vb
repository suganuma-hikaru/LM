' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI550V : TSMC在庫照会
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI550Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI550V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI550F

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI550G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMIControlV

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMIControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI550F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm
        Me._Frm = frm
        Me._Gcon = New LMIControlG(frm)

        'Validate共通クラスの設定
        Me._Vcon = New LMIControlV(handlerClass, DirectCast(frm, Form), Me._Gcon)

        Me._G = New LMI550G(handlerClass, frm)

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "権限チェック"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI550C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu
            Case LMI550C.EventShubetsu.SEARCH
                ' 検索
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = True
                End Select

        End Select

        If kengenFlg Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If

    End Function

#End Region

#Region "共通処理"

    ''' <summary>
    ''' 未選択チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>チェックリストの取得が前提</remarks>
    Friend Function IsSelectDataChk(ByVal chkList As ArrayList) As Boolean

        If Not Me._Vcon.IsSelectChk(chkList.Count()) Then
            Me.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

#End Region ' "共通処理"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理：入力チェック（単項目チェック）
    ''' </summary>
    ''' <param name="g"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSearchSingleCheck(ByVal g As LMI550G) As Boolean

        Dim sprDetailDef As LMI550G.sprDetailDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMI550G.sprDetailDefault)

        With Me._Frm

            ' 営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If Not MyBase.IsValidateCheck(.cmbEigyo) Then
                Return False
            End If

            ' 日付FROM
            If Not String.IsNullOrEmpty(.cmbSearchDate.TextValue) AndAlso
                Not .imdSearchDateFrom.IsDateFullByteCheck(8) Then
                MyBase.ShowMessage("E038", New String() {$"{ .cmbSearchDate.TextValue}From", "8"})
                Return False
            End If

            ' 日付TO
            If Not String.IsNullOrEmpty(.cmbSearchDate.TextValue) AndAlso
                Not .imdSearchDateTo.IsDateFullByteCheck(8) Then
                MyBase.ShowMessage("E038", New String() {$"{ .cmbSearchDate.TextValue}To", "8"})
                Return False
            End If

            ' 荷主コード(大)
            .txtCustCdL.ItemName() = "荷主コード(大)"
            .txtCustCdL.IsHissuCheck() = True
            .txtCustCdL.IsForbiddenWordsCheck() = True
            .txtCustCdL.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Return False
            End If

            ' 荷主コード(中)
            .txtCustCdM.ItemName() = "荷主コード(中)"
            .txtCustCdM.IsHissuCheck() = True
            .txtCustCdM.IsForbiddenWordsCheck() = True
            .txtCustCdM.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                Return False
            End If

            ' スプレッドの値をTrim
            Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0)

        End With

        Return True

    End Function

    ''' <summary>
    ''' 検索処理：入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSearchKanrenCheck() As Boolean

        With Me._Frm

            Dim custCdL As String = ""
            Dim custCdM As String = ""
            If String.IsNullOrEmpty(.txtCustCdL.TextValue) = False Then
                custCdL = .txtCustCdL.TextValue
            End If
            If String.IsNullOrEmpty(.txtCustCdM.TextValue) = False Then
                custCdM = .txtCustCdM.TextValue
            End If

            ' 荷主コード/荷主名 (大/中) 初期値設定(兼存在チェック)
            If Not Me._G.SetInitControlCust(Me._Frm, custCdL, custCdM) Then
                .txtCustCdL.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                .txtCustCdM.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                .txtCustCdL.Focus()
                Me.ShowMessage("E773")
                Return False
            End If

            ' 日付FROM/日付TO
            If Not String.IsNullOrEmpty(.cmbSearchDate.TextValue) AndAlso
                Not String.IsNullOrEmpty(.imdSearchDateFrom.TextValue) AndAlso
                Not String.IsNullOrEmpty(.imdSearchDateTo.TextValue) Then
                If Convert.ToInt32(.imdSearchDateTo.TextValue) < Convert.ToInt32(.imdSearchDateFrom.TextValue) Then
                    '大小チェック
                    Me.ShowMessage("E039", New String() {$"{ .cmbSearchDate.TextValue}To", $"{ .cmbSearchDate.TextValue}From"})
                    .imdSearchDateFrom.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdSearchDateTo.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdSearchDateFrom.Focus()
                    Return False
                End If
            End If

            ' 経過日数FROM/経過日数TO
            If Not String.IsNullOrEmpty(.numKeikaFrom.TextValue) AndAlso
                Not String.IsNullOrEmpty(.numKeikaTo.TextValue) Then
                If Convert.ToInt32(.numKeikaTo.TextValue) < Convert.ToInt32(.numKeikaFrom.TextValue) Then
                    '大小チェック
                    Me.ShowMessage("E505", New String() {"経過日数To", "経過日数From"})
                    .numKeikaFrom.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .numKeikaTo.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .numKeikaFrom.Focus()
                    Return False
                End If
            End If

        End With

        Return True

    End Function

#End Region

#End Region 'Method

End Class
