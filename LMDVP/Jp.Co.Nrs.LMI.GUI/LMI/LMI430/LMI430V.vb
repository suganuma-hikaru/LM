' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI430  : シリンダー輸入取込
'  作  成  者       :  [inoue]
' ==========================================================================

Option Explicit On

Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports FarPoint.Win.Spread
Imports System.IO

Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI430Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI430V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI430F = Nothing

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMIControlV = Nothing

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI430F, ByVal v As LMIControlV, ByVal g As LMI430G)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

    End Sub

#End Region 'Constructor

#Region "Method"


    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI430C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim authLevel As String = LMUserInfoManager.GetAuthoLv
        Dim isAllow As Boolean = False

        Select Case eventShubetsu

            ' 取込, Excel出力 行削除
            Case LMI430C.EventShubetsu.LOAD_FILE _
               , LMI430C.EventShubetsu.CREATE_EXCEL _
               , LMI430C.EventShubetsu.DELETE_SELECTED_ROW

                '10:閲覧者、50:外部の場合エラー
                Select Case authLevel
                    Case LMConst.AuthoKBN.EDIT _
                       , LMConst.AuthoKBN.EDIT_UP _
                       , LMConst.AuthoKBN.LEADER _
                       , LMConst.AuthoKBN.MANAGER

                        ' 許可=> 20:入力者(一般), 25:入力者(上級), 30:管理職, 40:システム管理者
                        isAllow = True

                End Select
            Case LMI430C.EventShubetsu.SEARCH _
               , LMI430C.EventShubetsu.OPEN_MASTER

                Select Case authLevel
                    Case LMConst.AuthoKBN.EDIT _
                       , LMConst.AuthoKBN.EDIT_UP _
                       , LMConst.AuthoKBN.LEADER _
                       , LMConst.AuthoKBN.MANAGER _
                       , LMConst.AuthoKBN.VIEW

                        ' 許可=> 10:閲覧者、20:入力者(一般), 25:入力者(上級), 30:管理職, 40:システム管理者
                        isAllow = True

                End Select


            Case LMI430C.EventShubetsu.CLOSE_FORM        '閉じる
                'すべての権限許可
                isAllow = True

            Case Else
                'すべての権限許可
                isAllow = True

        End Select

        Return isAllow

    End Function

    ''' <summary>
    ''' 単項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI430C.EventShubetsu) As Boolean

        '【単項目チェック】
        With Me._Frm


            If (LMI430C.EventShubetsu.LOAD_FILE.Equals(eventShubetsu) OrElse _
                LMI430C.EventShubetsu.SEARCH.Equals(eventShubetsu)) Then


                .txtCustCdL.ItemName() = LMI430C.CUST_CD_L_TEXT
                .txtCustCdL.IsHissuCheck() = True
                .txtCustCdL.IsForbiddenWordsCheck() = True
                .txtCustCdL.IsByteCheck() = LMI430C.CUST_CD_L_LENGTH
                If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    Return False
                End If


                .txtCustCdM.ItemName() = LMI430C.CUST_CD_M_TEXT
                .txtCustCdM.IsHissuCheck() = True
                .txtCustCdM.IsForbiddenWordsCheck() = True
                .txtCustCdM.IsByteCheck() = LMI430C.CUST_CD_M_LENGTH
                If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                    Return False
                End If

            End If


            If LMI430C.EventShubetsu.LOAD_FILE.Equals(eventShubetsu) = True Then
                .imdInkaDate.ItemName() = .lblInputInkaDate.Text
                .imdInkaDate.IsHissuCheck() = True
                .imdInkaDate.IsDateFullByteCheck()
                If MyBase.IsValidateCheck(.imdInkaDate) = False Then
                    Return False
                End If
            End If


        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI430C.EventShubetsu) As Boolean
        Return True
    End Function


    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMI430C.ActionType) As Boolean

        ''フォーカス位置がない場合、スルー
        'If String.IsNullOrEmpty(objNm) = True Then
        '    Return False
        'End If

        Dim ctl1 As Win.InputMan.LMImTextBox = Nothing
        Dim ctl2 As Win.InputMan.LMImTextBox = Nothing
        Dim ctl3 As Win.InputMan.LMImTextBox = Nothing
        Dim msg1 As String = String.Empty
        Dim msg2 As String = String.Empty
        Dim msg3 As String = String.Empty

        With Me._Frm

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    ctl1 = .txtCustCdL
                    msg1 = LMI430C.CUST_CD_L_TEXT
                    ctl2 = .txtCustCdM
                    msg2 = LMI430C.CUST_CD_M_TEXT

                    'コードが空なら名称を消す
                    If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True _
                    And String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                        .lblCustNM.TextValue = String.Empty
                    End If

            End Select

            'Nothing判定用
            Dim ctlChk As Boolean = ctl2 Is Nothing

            Dim ctlChk2 As Boolean = ctl3 Is Nothing

            'フォーカス位置が参照可能でない場合、エラー
            If (ctl1 Is Nothing = True OrElse ctl1.ReadOnly = True) _
                OrElse (ctlChk = False AndAlso ctl2.ReadOnly = True) Then

                Select Case actionType

                    Case LMI430C.ActionType.MASTER

                        Return _Vcon.SetFocusErrMessage()

                    Case LMI430C.ActionType.ENTER

                        'Enterの場合はメッセージは設定しない
                        Return False

                End Select


            End If

            '禁止文字チェック(1つ目のコントロール)
            ctl1.ItemName = msg1
            ctl1.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(ctl1) = False Then
                Return False
            End If

            '禁止文字チェック(2つ目のコントロール)
            If ctlChk = False Then
                ctl2.ItemName = msg2
                ctl2.IsForbiddenWordsCheck = True
                If MyBase.IsValidateCheck(ctl2) = False Then
                    Return False
                End If
            End If

            '禁止文字チェック(2つ目のコントロール)
            If ctlChk2 = False Then
                ctl3.ItemName = msg3
                ctl3.IsForbiddenWordsCheck = True
                If MyBase.IsValidateCheck(ctl3) = False Then
                    Return False
                End If
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 未選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectDataChk() As Boolean

        '選択チェック
        If Me._Vcon.IsSelectChk(Me.GetCheckList().Count()) = False Then
            Me.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function


#Region "選択行取得"
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCheckList() As ArrayList

        Return Me._Vcon.SprSelectList2(LMI430C.SprColumnIndex.DEF _
                                     , Me._Frm.sprDetails)

    End Function

#End Region
#End Region 'Method

End Class
