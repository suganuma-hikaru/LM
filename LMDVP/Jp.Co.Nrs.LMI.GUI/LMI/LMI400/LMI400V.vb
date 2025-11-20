' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI400V : セット品マスタメンテ
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread

''' <summary>
''' LMI400Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMI400V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI400F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMIControlV

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI400F, ByVal v As LMIControlV, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._ControlV = v

        'Gamen共通クラスの設定
        Me._ControlG = g

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI400C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI400C.EventShubetsu.SHINKI       '新規処理
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

            Case LMI400C.EventShubetsu.HENSHU       '編集
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

            Case LMI400C.EventShubetsu.KENSAKU         '検索
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


            Case LMI400C.EventShubetsu.MASTEROPEN      'マスタ参照
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMI400C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMI400C.EventShubetsu.TOJIRU          '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMI400C.EventShubetsu.DOUBLE_CLICK    'ダブルクリック
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

            Case LMI400C.EventShubetsu.ENTER           'Enter
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

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        If kengenFlg = True Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If
        Return False


    End Function

    ''' <summary>
    ''' 単項目/関連チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsInputChk(ByVal eventShubetsu As LMI400C.EventShubetsu, Optional ByVal arr As ArrayList = Nothing) As Boolean

        '単項目/関連チェック
        If Me.IsSingleChk(eventShubetsu, arr) = False OrElse Me.IsSaveCheck(eventShubetsu) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 基本メッセージ設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetBaseMsg()

        Select Case Me._Frm.lblSituation.DispMode
            Case DispMode.INIT

                MyBase.ShowMessage("G007")

            Case DispMode.VIEW

                MyBase.ShowMessage("G013")

            Case DispMode.EDIT

                MyBase.ShowMessage("G003")

        End Select

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCheckList(ByVal eventShubetsu As LMI400C.EventShubetsu) As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = 0
        Dim spr As SheetView = Nothing

        Select Case eventShubetsu
            Case LMI400C.EventShubetsu.SAKUJO_HUKKATU
                defNo = LMI400C.SprSearchColumnIndex.DEF
                spr = Me._Frm.sprSearch.ActiveSheet

            Case LMI400C.EventShubetsu.ROW_DEL
                defNo = LMI400C.SprDetailColumnIndex.DEF
                spr = Me._Frm.sprDetail.ActiveSheet

        End Select

        Return Me.GetCheckList(defNo, spr)

    End Function

    ''' <summary>
    ''' スプレッド明細行のチェックリスト(RowIndex)取得
    ''' </summary>
    ''' <param name="activeSheet">スプレッドシート</param>
    ''' <param name="defNo">レコードチェックボックスのカラム№</param>
    ''' <returns>チェックリスト</returns>
    ''' <remarks></remarks>
    Friend Function GetCheckList(ByVal defNo As Integer, ByVal activeSheet As SheetView) As ArrayList

        'チェック件数取得
        With activeSheet

            Dim arr As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If Me._ControlV.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    arr.Add(i)
                End If
            Next

            Return arr

        End With

    End Function

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSingleChk(ByVal eventShubetsu As LMI400C.EventShubetsu, ByVal arr As ArrayList) As Boolean

        With Me._Frm

            Dim sCell As LMValidatableCells = New LMValidatableCells(.sprSearch)
            Dim dCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            Select Case eventShubetsu

                Case LMI400C.EventShubetsu.SAKUJO_HUKKATU '削除・復活

                    '選択チェック
                    If Me._ControlV.IsSelectChk(arr.Count) = False Then
                        MyBase.ShowMessage("E009")
                        Return False
                    End If

                Case LMI400C.EventShubetsu.KENSAKU '検索

                    '【親コード】
                    sCell.SetValidateCell(0, LMI400G.sprSearchDef.OYA_CD.ColNo)
                    sCell.ItemName = "親コード"
                    sCell.IsForbiddenWordsCheck = True
                    sCell.IsByteCheck = 20
                    If MyBase.IsValidateCheck(sCell) = False Then
                        Return False
                    End If

                    '【親名称】
                    sCell.SetValidateCell(0, LMI400G.sprSearchDef.OYA_NM.ColNo)
                    sCell.ItemName = "親名称"
                    sCell.IsForbiddenWordsCheck = True
                    sCell.IsByteCheck = 60
                    If MyBase.IsValidateCheck(sCell) = False Then
                        Return False
                    End If

                Case LMI400C.EventShubetsu.HOZON '保存処理

                    '【営業所コード】
                    .cmbNrsBr.ItemName = "営業所コード"
                    .cmbNrsBr.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbNrsBr) = False Then
                        Me._ControlV.SetErrorControl(.cmbNrsBr)
                        Return False
                    End If

                    '【親コード】
                    .txtOyaCd.ItemName = "親コード"
                    .txtOyaCd.IsHissuCheck = True
                    .txtOyaCd.IsForbiddenWordsCheck = True
                    .txtOyaCd.IsByteCheck = 20
                    If MyBase.IsValidateCheck(.txtOyaCd) = False Then
                        Me._ControlV.SetErrorControl(.txtOyaCd)
                        Return False
                    End If

                    '【親名称】
                    .txtOyaNm.ItemName = "親名称"
                    .txtOyaNm.IsHissuCheck = True
                    .txtOyaNm.IsForbiddenWordsCheck = True
                    .txtOyaNm.IsByteCheck = 60
                    If MyBase.IsValidateCheck(.txtOyaNm) = False Then
                        Me._ControlV.SetErrorControl(.txtOyaNm)
                        Return False
                    End If

                    For i As Integer = 0 To .sprDetail.ActiveSheet.Rows.Count - 1

                        '【子コード】
                        dCell.SetValidateCell(i, LMI400G.sprDetailDef.KO_CD.ColNo)
                        dCell.ItemName = "子コード"
                        dCell.IsHissuCheck = True
                        dCell.IsForbiddenWordsCheck = True
                        dCell.IsByteCheck = 20
                        If MyBase.IsValidateCheck(dCell) = False Then
                            Return False
                        End If

                        '【セット個数】
                        dCell.SetValidateCell(i, LMI400G.sprDetailDef.KOSU.ColNo)
                        dCell.ItemName = "セット個数"
                        dCell.IsByteCheck = 5
                        If MyBase.IsValidateCheck(dCell) = False Then
                            Return False
                        End If

                        Dim kosu As Integer = Convert.ToInt32(Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMI400G.sprDetailDef.KOSU.ColNo)))
                        If kosu < 1 Then
                            MyBase.ShowMessage("E182", New String() {"個数", "1"})
                            Return False
                        End If

                    Next

            End Select

            Return True

        End With

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveCheck(ByVal eventShubetsu As LMI400C.EventShubetsu) As Boolean

        With Me._Frm

            Dim max As Integer = .sprDetail.ActiveSheet.RowCount - 1

            Select Case eventShubetsu

                Case LMI400C.EventShubetsu.HOZON

                    If max < 0 Then
                        MyBase.ShowMessage("E469", New String() {"明細行"})
                        Return False
                    End If

                    '要望対応:1881 yamanaka 2013.02.20 Start
                    Dim koCd As String = String.Empty
                    Dim koCdCheck As String = String.Empty

                    For i As Integer = 0 To max
                        koCd = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMI400G.sprDetailDef.KO_CD.ColNo))

                        For j As Integer = i + 1 To max
                            koCdCheck = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(j, LMI400G.sprDetailDef.KO_CD.ColNo))

                            If koCd.Equals(koCdCheck) = True Then
                                MyBase.ShowMessage("E160", New String() {"スプレッド内", String.Concat("子コード:「", koCdCheck, "」")})
                                Return False
                            End If
                        Next
                    Next
                    '要望対応:1881 yamanaka 2013.02.20 End

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    '''クリア処理を行う
    ''' </summary>
    ''' <param name="ctl">クリア対象項目</param>
    ''' <remarks></remarks>
    Private Sub ClearControl(ByVal ctl As Win.InputMan.LMImTextBox(), _
                             ByVal clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl())

        'クリアコントロール未設定の場合、処理終了
        If clearCtl Is Nothing Then
            Exit Sub
        End If

        Dim clearMax As Integer = clearCtl.Length - 1

        'エディット系コントロールのクリア処理を行う
        For index As Integer = 0 To clearMax

            'コントロール別にクリア処理を行う
            If TypeOf clearCtl(index) Is Win.InputMan.LMImCombo = True Then

                DirectCast(clearCtl(index), Win.InputMan.LMImCombo).SelectedValue = String.Empty

            ElseIf TypeOf clearCtl(index) Is Win.InputMan.LMComboKubun = True Then

                DirectCast(clearCtl(index), Win.InputMan.LMComboKubun).SelectedValue = String.Empty

            ElseIf TypeOf clearCtl(index) Is Win.InputMan.LMImNumber = True Then

                DirectCast(clearCtl(index), Win.InputMan.LMImNumber).Value = 0

            Else

                clearCtl(index).TextValue = String.Empty

            End If

        Next

    End Sub

#End Region

#End Region

End Class
