' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB070V : 写真選択
'  作  成  者       :  matsumoto
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.LMGUIUtility
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB070Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMB070V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB070F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMBControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB070F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = New LMBControlV(handlerClass, DirectCast(frm, Form))

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 単項目
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsInputChk(ByVal g As LMB070G) As Boolean

        '単項目チェック
        If Me.IsSingleCheck() = False Then
            Return False
        End If

        '関連チェック
        If Me.IsKanrenCheck() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 画面ヘッダー部単項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSingleCheck() As Boolean

        With Me._Frm

            '撮影日From
            If .imdSatsueiDateFrom.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"撮影日From", "8"})
                Return False
            End If

            '撮影日To
            If .imdSatsueiDateTo.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"撮影日To", "8"})
                Return False
            End If

            'キーワード検索
            .txtKeyword.ItemName() = .LmTitleLabel2.TextValue
            .txtKeyword.IsHissuCheck() = False
            .txtKeyword.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtKeyword) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 画面ヘッダー部関連チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsKanrenCheck() As Boolean

        With Me._Frm

            '撮影日From + 撮影日To
            '  撮影日Fromより撮影日Toが過去日の場合エラー
            '  いずれも設定済 である場合のみチェック
            If .imdSatsueiDateFrom.TextValue.Equals(String.Empty) = False _
                And .imdSatsueiDateTo.TextValue.Equals(String.Empty) = False Then

                If .imdSatsueiDateTo.TextValue < .imdSatsueiDateFrom.TextValue Then

                    '撮影日Fromより撮影日Toが過去日の場合エラー
                    Me.ShowMessage("E039", New String() {"撮影日To", "撮影日From"})
                    .imdSatsueiDateFrom.BackColorDef() = GetAttentionBackColor()
                    .imdSatsueiDateTo.BackColorDef() = GetAttentionBackColor()
                    .imdSatsueiDateFrom.Focus()
                    Return False

                End If

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 画像選択チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsSelectedCheck(ByVal g As LMB070G) As Boolean

        '画像コンテナ内の画像が選択されているかチェック
        Dim isSel As Boolean = Me.IsSelectedPhoto()

        If isSel = False Then
            MyBase.ShowMessage("E033")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 画像選択しているかチェック
    ''' </summary>
    ''' <returns></returns>
    Friend Function IsSelectedPhoto() As Boolean

        '画像コンテナ内の画像が選択されているかチェック
        Dim isSel As Boolean = False
        For Each ctl As Control In Me._Frm.pnlDetailIn.Controls

            '画像コンテナの場合
            If ctl.Name.IndexOf(LMB070C.CTLNM_PANELB) >= 0 Then

                For Each ctlIn As Control In ctl.Controls
                    '画像コントロールの場合
                    If ctlIn.Name.IndexOf(LMB070C.CTLNM_PHOTO) >= 0 Then
                        'If Not ctlIn.Tag Is Nothing AndAlso Not String.IsNullOrEmpty(ctlIn.Tag.ToString()) Then    'DEL 2023/08/18 037916
                        'ADD START 2023/08/18 037916
                        Dim lblNm As String = ctlIn.Name.Replace(LMB070C.CTLNM_PHOTO, LMB070C.CTLNM_PHOTOLABEL)
                        Dim lblCtrl() As Control = ctl.Controls.Find(lblNm, True)
                        If lblCtrl.Length > 0 Then
                            '選択画像の場合
                            If Not lblCtrl(0).Tag Is Nothing AndAlso Not String.IsNullOrEmpty(lblCtrl(0).Tag.ToString()) Then
                                'ADD END 2023/08/18 037916
                                isSel = True
                                Exit For
                            End If
                        End If  'ADD 2023/08/18 037916
                    End If
                Next

                If isSel Then
                    Exit For
                End If
            End If
        Next

        Return isSel

    End Function

#End Region 'Method

End Class
