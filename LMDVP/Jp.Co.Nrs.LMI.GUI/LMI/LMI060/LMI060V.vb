' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI060V : 三井化学ポリウレタン運賃計算「危険品一割増」処理
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI060Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI060V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI060F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI060F, ByVal v As LMIControlV, ByVal g As LMI060G)

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
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI060C.EventShubetsu) As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        'マイナス０を変換
        Call Me.ZeroTextValue()

        '【単項目チェック】
        With Me._Frm

            If LMI060C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI060C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                '期間FROM
                .imdDateFrom.ItemName() = "期間From"
                .imdDateFrom.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdDateFrom) = False Then
                    Return False
                End If
                If .imdDateFrom.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {"期間From", "8"})
                    Return False
                End If
            End If

            If LMI060C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI060C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                '請求月TO
                .imdDateTo.ItemName() = "期間To"
                .imdDateTo.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdDateTo) = False Then
                    Return False
                End If
                If .imdDateTo.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {"期間To", "8"})
                    Return False
                End If
            End If

            '要望番号:1482 KIM 2012.10.10 START
            'If LMI060C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
            '    LMI060C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
            '    '荷主コード(大)
            '    If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True Then
            '        MyBase.ShowMessage("E001", New String() {"荷主コード(大)"})
            '        Return False
            '    End If
            'End If

            'If LMI060C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
            '    LMI060C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
            '    '荷主コード(中)
            '    If String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
            '        MyBase.ShowMessage("E001", New String() {"荷主コード(中)"})
            '        Return False
            '    End If
            'End If
            '要望番号:1482 KIM 2012.10.10 END

            If LMI060C.EventShubetsu.MAKE.Equals(eventShubetsu) = True Then
                '作成種別
                .cmbMake.ItemName() = .lblTitleMake.Text
                .cmbMake.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbMake) = False Then
                    Return False
                End If
            End If

            If LMI060C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                '印刷種別
                .cmbPrint.ItemName() = .lblTitlePrint.Text
                .cmbPrint.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbPrint) = False Then
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
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI060C.EventShubetsu) As Boolean

        '【関連項目チェック】
        With Me._Frm

            If LMI060C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI060C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                If .imdDateTo.TextValue < .imdDateFrom.TextValue Then
                    '期間FROM ＋ 期間TO
                    MyBase.ShowMessage("E166", New String() {"期間To", "期間From"})
                    .imdDateTo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.imdDateFrom)
                    Return False
                Else
                    .imdDateFrom.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    .imdDateTo.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                End If
            End If

            '要望番号:1482 KIM 2012.10.10 START
            '未選択チェック
            If LMI060C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
              LMI060C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                If Me._Vcon.SprSelectList2(LMI060C.SprColumnIndex.DEF, Me._Frm.sprDetail).Count = 0 Then
                    MyBase.ShowMessage("E009")
                    Return False
                End If
            End If
            '要望番号:1482 KIM 2012.10.10 END

        End With

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI060C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI060C.EventShubetsu.CLOSE        '照会
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

            Case LMI060C.EventShubetsu.CLOSE        '閉じる
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

            Case LMI060C.EventShubetsu.MAKE         '作成
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

            Case LMI060C.EventShubetsu.PRINT        '印刷
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

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        Return kengenFlg

    End Function

    ''' <summary>
    ''' 項目のTrim処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceTextValue()

        With Me._Frm
            '各項目のTrim処理

        End With

    End Sub

    ''' <summary>
    ''' マイナス０を変換
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ZeroTextValue()

        With Me._Frm
            '各項目のTrim処理

        End With

    End Sub

#End Region 'Method

End Class
