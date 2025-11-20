' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI460  : ローム　請求先コード変更
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI460Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI460V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI460F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMIControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI460F, ByVal v As LMIControlV, ByVal g As LMI460G)

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
    ''' 単項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI460C.EventShubetsu) As Boolean

        '【単項目チェック】
        With Me._Frm

            ''出荷予定日 From
            .imdOutkaDateFrom.IsHissuCheck() = True
            .imdOutkaDateFrom.ItemName() = "出荷予定日(FROM)"
            If MyBase.IsValidateCheck(.imdOutkaDateFrom) = False Then
                Return False
            End If
            If .imdOutkaDateFrom.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"出荷予定日(FROM)", "8"})
                Return False
            End If
            '    End If

            '出荷予定日 To
            .imdOutkaDateTo.IsHissuCheck() = True
            .imdOutkaDateTo.ItemName() = "出荷予定日(TO)"
            If MyBase.IsValidateCheck(.imdOutkaDateTo) = False Then
                Return False
            End If
            If .imdOutkaDateTo.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"出荷予定日(TO)", "8"})
                Return False
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
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI460C.EventShubetsu) As Boolean


        '【関連項目チェック】
        With Me._Frm

            '荷主コード　
            If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True Then
                Me.ShowMessage("E223", New String() {"荷主コードの設定がされてない為実行"})
                .txtCustCdL.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                .txtCustCdL.Focus()
                Return False

            End If

            If String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                Me.ShowMessage("E223", New String() {"荷主コードの設定がされてない為実行"})
                .txtCustCdM.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                .txtCustCdM.Focus()
                Return False

            End If

            '出荷予定日
            If String.IsNullOrEmpty(.imdOutkaDateFrom.TextValue) = False AndAlso String.IsNullOrEmpty(.imdOutkaDateTo.TextValue) = False Then
                If Convert.ToInt32(.imdOutkaDateTo.TextValue) < Convert.ToInt32(.imdOutkaDateFrom.TextValue) Then
                    'FromよりToが過去日の場合エラー
                    Me.ShowMessage("E039", New String() {"出荷予定日To ", "出荷予定日From "})
                    .imdOutkaDateFrom.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdOutkaDateTo.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdOutkaDateFrom.Focus()
                    Return False
                End If

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI460C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI460C.EventShubetsu.MASTER       'マスタ参照
                '10:閲覧者の場合エラー
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

            Case LMI460C.EventShubetsu.JIKKO        '実行
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

            Case LMI460C.EventShubetsu.CLOSE        '閉じる
                'すべての権限許可
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
                        kengenFlg = True
                End Select

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        Return kengenFlg

    End Function

#End Region 'Method

End Class
