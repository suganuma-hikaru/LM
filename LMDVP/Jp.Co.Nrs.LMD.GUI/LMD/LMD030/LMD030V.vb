' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMD     : 在庫管理
'  プログラムID     :  LMD030V : 在庫履歴
'  作  成  者       :  金ヘスル
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD030Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMD030V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD030F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMDControlV

    ''' <summary>
    ''' チェックリストを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD030F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm
        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = New LMDControlV(handlerClass, DirectCast(frm, Form))

        Me._ChkList = New ArrayList()

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "内部メソッド"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMD030C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMD030C.EventShubetsu.DEL             '削除
                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMD030C.EventShubetsu.KENSAKU         '検索
                '50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMD030C.EventShubetsu.CLOSE           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        If kengenFlg = True Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMD030C.SprColumnIndex.DEF

        '選択された行の行番号を取得
        Me._ChkList = Me._Vcon.SprSelectList(defNo, Me._Frm.sprDetail)

        Return Me._ChkList

    End Function

    ''' <summary>
    ''' 選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectDataChk() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Call Me.getCheckList()

        '選択チェック
        If Me._Vcon.IsSelectChk(Me._ChkList.Count()) = False Then
            Me.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 取消可能チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsTorikesiChk() As Boolean

        '格納変数初期化
        Dim num As Integer = 0
        Dim syubetu As String = String.Empty

        Dim max As Integer = Me._ChkList.Count() - 1

        With Me._Frm.sprDetail.ActiveSheet

            For i As Integer = 0 To max

                num = Convert.ToInt32(Me._ChkList(i))

                '種別取得
                syubetu = .Cells(num, LMD030G.sprDetailDef.SYUBETU.ColNo).Text()

                If syubetu.Equals("移入") = False Then
                    '種別チェック
                    Me.ShowMessage("E200")
                    Return False
                End If

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 後続行チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsNextRowChk() As Boolean

        '格納変数
        Dim syubetu As String = String.Empty '種別
        Dim kb As String = String.Empty      '区分

        Dim max As Integer = Me._Frm.sprDetail.ActiveSheet.Rows.Count() - 1

        With Me._Frm.sprDetail.ActiveSheet

            For i As Integer = Convert.ToInt32(Me._ChkList(0)) To max

                syubetu = .Cells(i, LMD030G.sprDetailDef.SYUBETU.ColNo).Text()   '種別取得
                kb = .Cells(i, LMD030G.sprDetailDef.STATE_KB.ColNo).Text()       '区分取得

                If syubetu.Equals("振替") = True And kb.Equals("消") = False Then
                    '後続行振替チェック
                    Me.ShowMessage("E202", New String() {"振替"})
                    Return False
                ElseIf syubetu.Equals("出荷") = True Then
                    '後続行出荷チェック
                    If kb.Equals("予") = True Then
                        Me.ShowMessage("E202", New String() {"出荷予定"})
                        Return False
                    ElseIf kb.Equals("消") = False Then
                        Me.ShowMessage("E202", New String() {"出荷"})
                        Return False
                    End If
                ElseIf syubetu.Equals("移出") = True Then
                    '後続行移出チェック
                    Me.ShowMessage("E202", New String() {"移出"})
                    Return False
                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="ctl">エラーコントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal ctl As Control)

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor()

        If TypeOf ctl Is Win.InputMan.LMImTextBox = True Then
            If DirectCast(ctl, Win.InputMan.LMImTextBox).ReadOnly = False Then
                DirectCast(ctl, Win.InputMan.LMImTextBox).BackColorDef = errorColor
            End If
        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then
            If DirectCast(ctl, Win.InputMan.LMComboKubun).ReadOnly = False Then
                DirectCast(ctl, Win.InputMan.LMComboKubun).BackColorDef = errorColor
            End If
        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then
            If DirectCast(ctl, Win.InputMan.LMImNumber).ReadOnly = False Then
                DirectCast(ctl, Win.InputMan.LMImNumber).BackColorDef = errorColor
            End If
        ElseIf TypeOf ctl Is Win.InputMan.LMImDate = True Then
            If DirectCast(ctl, Win.InputMan.LMImDate).ReadOnly = False Then
                DirectCast(ctl, Win.InputMan.LMImDate).BackColorDef = errorColor
            End If
        End If

        ctl.Focus()
        ctl.Select()

    End Sub

#End Region '内部メソッド

#End Region 'Method

End Class
