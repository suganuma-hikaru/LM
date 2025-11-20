' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI440  : 輸出ファイル編集
'  作  成  者       :  [inoue]
' ==========================================================================
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
''' LMI440Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI440V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI440F = Nothing

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI440F, ByVal v As LMIControlV, ByVal g As LMI440G)

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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI440C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim authLevel As String = LMUserInfoManager.GetAuthoLv
        Dim isAllow As Boolean = False

        Select Case eventShubetsu

            ' 取込, Excel出力 行削除
            Case LMI440C.EventShubetsu.EXECUTE

                '10:閲覧者、50:外部の場合エラー
                Select Case authLevel
                    Case LMConst.AuthoKBN.EDIT _
                       , LMConst.AuthoKBN.EDIT_UP _
                       , LMConst.AuthoKBN.LEADER _
                       , LMConst.AuthoKBN.MANAGER

                        ' 許可=> 20:入力者(一般), 25:入力者(上級), 30:管理職, 40:システム管理者
                        isAllow = True

                End Select

            Case LMI440C.EventShubetsu.CLOSE_FORM        '閉じる
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
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI440C.EventShubetsu) As Boolean

        '【単項目チェック】
        With Me._Frm

            If (LMI440C.EventShubetsu.EXECUTE.Equals(eventShubetsu)) Then

                .cmbAction.ItemName() = .lblActionName.Text
                .cmbAction.IsHissuCheck() = True
                .cmbAction.IsForbiddenWordsCheck() = False
                If MyBase.IsValidateCheck(.cmbAction) = False Then
                    Return False
                End If

                .imdArrPlanDateFrom.ItemName() = String.Concat(.lblArrPlanDate.Text, LMI440C.FROM_TEXT)
                .imdArrPlanDateFrom.IsHissuCheck() = False
                If (.imdArrPlanDateFrom.IsDateFullByteCheck(8) = False) Then
                    MyBase.ShowMessage("E038", New String() {.lblArrPlanDate.Text, "8"})
                    Return False
                End If
                If (MyBase.IsValidateCheck(.imdArrPlanDateFrom) = False) Then

                    Return False
                End If


                .imdArrPlanDateTo.ItemName() = String.Concat(.lblArrPlanDate.Text, LMI440C.TO_TEXT)
                .imdArrPlanDateTo.IsHissuCheck() = False
                If (.imdArrPlanDateTo.IsDateFullByteCheck(8) = False) Then
                    MyBase.ShowMessage("E038", New String() {.lblArrPlanDate.Text, "8"})
                    Return False
                End If

                If (MyBase.IsValidateCheck(.imdArrPlanDateTo) = False) Then

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
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI440C.EventShubetsu) As Boolean
        Return True
    End Function

#End Region 'Method

End Class
