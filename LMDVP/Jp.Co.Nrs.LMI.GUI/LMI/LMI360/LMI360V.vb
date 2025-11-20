' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI360  : ＤＩＣ運賃請求明細書作成
'  作  成  者       :  [篠原]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI360Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI360V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI360F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI360F, ByVal v As LMIControlV, ByVal g As LMI360G)

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
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI360C.EventShubetsu) As Boolean

        '【単項目チェック】
        With Me._Frm

            If LMI360C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI360C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                '荷主コード(大)
                .txtCustCdL.ItemName() = "荷主コード(大)"
                .txtCustCdL.IsHissuCheck() = True
                .txtCustCdL.IsForbiddenWordsCheck() = True
                .txtCustCdL.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    Return False
                End If
            End If

            If LMI360C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
                .txtCustCdL.ItemName() = "荷主コード(大)"
                .txtCustCdL.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    Return False
                End If
            End If

            If LMI360C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI360C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                '荷主コード(中)
                .txtCustCdM.ItemName() = "荷主コード(中)"
                .txtCustCdM.IsHissuCheck() = True
                .txtCustCdM.IsForbiddenWordsCheck() = True
                .txtCustCdM.IsFullByteCheck() = 2
                If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                    Return False
                End If
            End If

            If LMI360C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
                '荷主コード(中)
                .txtCustCdM.ItemName() = "荷主コード(中)"
                .txtCustCdM.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                    Return False
                End If
            End If

            If LMI360C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI360C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                'LMI360C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                'LMI360C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                '出荷日FROM
                .imdDateFrom.ItemName() = "出荷日From"
                .imdDateFrom.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdDateFrom) = False Then
                    Return False
                End If
                If .imdDateFrom.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {"出荷日From", "8"})
                    Return False
                End If
            End If

            If LMI360C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
               LMI360C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                'LMI360C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                'LMI360C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                '出荷日TO
                .imdDateTo.ItemName() = "出荷日To"
                .imdDateTo.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdDateTo) = False Then
                    Return False
                End If
                If .imdDateTo.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {"出荷日To", "8"})
                    Return False
                End If
            End If

            'If LMI360C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
            '    '実行種別
            '    .cmbJikko.ItemName() = .lblTitleJikko.Text
            '    .cmbJikko.IsHissuCheck() = True
            '    If MyBase.IsValidateCheck(.cmbJikko) = False Then
            '        Return False
            '    End If
            'End If

            If LMI360C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                '印刷種別
                .cmbPrint.ItemName() = .lblTitlePrint.Text
                .cmbPrint.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbPrint) = False Then
                    Return False
                End If
            End If

            'If LMI360C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
            '    '路線
            '    .cmbRosen.ItemName() = .lblTitleRosen.Text
            '    .cmbRosen.IsHissuCheck() = True
            '    If MyBase.IsValidateCheck(.cmbRosen) = False Then
            '        Return False
            '    End If
            'End If

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
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI360C.EventShubetsu) As Boolean


        '【関連項目チェック】
        With Me._Frm
            Dim eventNm As String = String.Empty
            'If LMI360C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
            'eventNm = LMI360C.EVENTNAME_JIKKO
            'ElseIf LMI360C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
            If LMI360C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
                eventNm = LMI360C.EVENTNAME_MASTER
            ElseIf LMI360C.EventShubetsu.CLOSE.Equals(eventShubetsu) = True Then
                eventNm = LMI360C.EVENTNAME_CLOSE
            ElseIf LMI360C.EventShubetsu.MAKE.Equals(eventShubetsu) = True Then
                eventNm = LMI360C.EVENTNAME_MAKE
            ElseIf LMI360C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                eventNm = LMI360C.EVENTNAME_PRINT
            End If

            If LMI360C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI360C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                'LMI360C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                'LMI360C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                If .imdDateTo.TextValue < .imdDateFrom.TextValue Then
                    '出荷日FROM ＋ 出荷日TO
                    MyBase.ShowMessage("E166", New String() {"出荷日To", "出荷日From"})
                    .imdDateTo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.imdDateFrom)
                    Return False
                Else
                    .imdDateFrom.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    .imdDateTo.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI360C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI360C.EventShubetsu.MASTER       'マスタ参照
                '10:閲覧者の場合エラー
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

            Case LMI360C.EventShubetsu.MAKE         '作成
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

                'Case LMI360C.EventShubetsu.JIKKO        '実行
                '    '10:閲覧者、50:外部の場合エラー
                '    Select Case kengen
                '        Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                '            kengenFlg = False
                '        Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                '            kengenFlg = True
                '        Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                '            kengenFlg = True
                '        Case LMConst.AuthoKBN.LEADER    '30:管理職
                '            kengenFlg = True
                '        Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                '            kengenFlg = True
                '        Case LMConst.AuthoKBN.AGENT     '50:外部
                '            kengenFlg = False
                '    End Select

            Case LMI360C.EventShubetsu.PRINT        '印刷
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

            Case LMI360C.EventShubetsu.CLOSE        '閉じる
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
