' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMU       : 文書管理
'  プログラムID     :  LMU010V   : 文書管理画面
'  作  成  者       :  NRS)OHNO
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports System.IO
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMU010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMU010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMU010F

    ''' <summary>
    ''' このValidateクラスが紐付く画面クラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMU010G

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMU010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Gamenクラスの設定
        Me._ControlG = New LMU010G(handlerClass, frm)

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    '''共通チェック(権限チェック、ヘッダ部入力チェック)
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsCommonChk(ByVal flgForViewAuthority As Boolean) As Boolean

        ''権限チェックを行う
        'If IsAuthorityChk(LMU010C.EVENT_EDIT) = False Then
        '    Return False
        'End If

        'ヘッダ部入力チェックを行う
        If IsInputChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As String) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMU010C.EVENT_EDIT          '編集
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

            Case LMU010C.EVENT_ADD         '行追加
                '50:外部の場合エラー
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

            Case LMU010C.EVENT_DEL       '行削除
                '50:外部の場合エラー
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


            Case LMU010C.EVENT_CLOSE          '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMU010C.EVENT_DCLICK         'ダブルクリック
                '10:閲覧者、50:外部の場合エラー
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

    End Function

    ''' <summary>
    ''' 関連チェックをします。
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsEditPossibleChk(ByVal chkList As ArrayList, ByVal msg As String) As Boolean

        With Me._Frm

            Dim hashTbl As Hashtable = CType(chkList.Item(0), Hashtable)
            If String.IsNullOrEmpty(hashTbl.Item("System").ToString) = False AndAlso _
               hashTbl.Item("System").ToString.Equals(LMU010C.SYSTEM_LEASE) = True Then
                Dim strReplaceMsg(2) As String
                strReplaceMsg(0) = "Lease Data"
                strReplaceMsg(1) = msg
                Me.ShowMessage("E028", strReplaceMsg)
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' スプレッドの項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadInputChk() As Boolean

        With Me._Frm

            For i As Integer = 0 To .sprData.ActiveSheet.RowCount - 1

                If Me.SprSelectCheck(Me._ControlG.GetCellValue(.sprData.ActiveSheet.Cells(i, LMU010G.sprDataDef.DEF.ColNo))) = True Then

                    '【スプレッド検索行】
                    Dim vCell As LMValidatableCells = New LMValidatableCells(.sprData)

                    'File Type.
                    vCell.SetValidateCell(i, LMU010G.sprDataDef.FILE_TYPE.ColNo)
                    vCell.ItemName = "ファイルタイプ"
                    vCell.IsHissuCheck = True
                    If Me.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                    'Remark
                    vCell.SetValidateCell(i, LMU010G.sprDataDef.REMARK.ColNo)
                    vCell.ItemName = "備考"
                    vCell.IsHissuCheck = False
                    vCell.IsForbiddenWordsCheck = True
                    vCell.IsByteCheck = 40
                    If Me.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                End If

            Next

        End With

        Return True

    End Function
    ''' <summary>
    ''' ファイル名バイト数チェック　2014/01/21　大野追加
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFileNameByteChk(ByVal fileName As String) As Boolean

        If fileName.ToString.Length > 50 Then

            Me.ShowMessage("E023", New String() {"ファイル名", "50"})
            Return False
        End If

        Return True

    End Function
    ''' <summary>
    ''' スプレッド未選択チェック（選択行）
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="defNo">チェックボックス列</param>
    ''' <param name="chkList">選択行のレコード番号を設定したリスト(チェック結果を設定)</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadSelectionChk(ByVal spr As Win.Spread.LMSpread, _
                                         ByVal defNo As Integer, _
                                         ByVal fileNo As Integer, _
                                         ByVal sysNo As Integer, _
                                         ByRef chkList As ArrayList) As Boolean

        '選択行をチェック
        chkList = Me.SpdSelectCount(spr, defNo, fileNo, sysNo)

        If 0 = chkList.Count Then
            '選択チェックボックス0件選択時エラー
            Me.ShowMessage("E009")
            Return False
        ElseIf 1 < chkList.Count Then
            '選択チェックボックス1件以上選択時エラー
            Me.ShowMessage("E008")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' スプレッドでチェックボックスが付いた行数を調べる
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="defNo">チェックボックス列</param>
    ''' <returns>リスト</returns>
    ''' <remarks></remarks>
    Friend Function SpdSelectCount(ByVal spr As Win.Spread.LMSpread, ByVal defNo As Integer, ByVal fileNo As Integer, ByVal sysNo As Integer) As ArrayList

        With spr.ActiveSheet

            Dim list As ArrayList = New ArrayList()
            Dim hashTbl As Hashtable
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max

                If Me.SprSelectCheck(Me._ControlG.GetCellValue(.Cells(i, defNo))) = True Then

                    hashTbl = New Hashtable

                    '選択されたレコード番号を設定
                    hashTbl.Add("FileNo", Me._ControlG.GetCellValue(.Cells(i, fileNo)))
                    hashTbl.Add("System", Me._ControlG.GetCellValue(.Cells(i, sysNo)))
                    hashTbl.Add("SysUpdDate", Me._ControlG.GetCellValue(.Cells(i, LMU010G.sprDataDef.SYS_UPD_DATE.ColNo)))
                    hashTbl.Add("SysUpdTime", Me._ControlG.GetCellValue(.Cells(i, LMU010G.sprDataDef.SYS_UPD_TIME.ColNo)))
                    list.Add(hashTbl)

                End If

            Next

            Return list

        End With

    End Function

    ''' <summary>
    ''' スプレッドを選択しているかを判定
    ''' </summary>
    ''' <param name="value">判定値</param>
    ''' <returns>
    ''' True :チェック有
    ''' False:チェック無
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function SprSelectCheck(ByVal value As String) As Boolean

        'チェックが入っている場合
        Return Me._ControlG.changeBooleanCheckBox(value)

    End Function

    ''' <summary>
    ''' ファイル存在チェックをします。
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsFileExistChk1(ByVal strLocal As String, ByVal strTarget As String) As Boolean

        Dim strReplaceMsg(1) As String

        If File.Exists(strLocal) = False Then
            strReplaceMsg(0) = strLocal.Substring(strLocal.LastIndexOf("\") + 1)
            Me.ShowMessage("E254", strReplaceMsg)
            Return False
        ElseIf File.Exists(strTarget) = True Then
            strReplaceMsg(0) = strTarget.Substring(strTarget.LastIndexOf("\") + 1)
            Me.ShowMessage("E255", strReplaceMsg)
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' ファイル存在チェックをします。
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsFileExistChk2(ByVal strTarget As String) As Boolean

        Dim strReplaceMsg(1) As String

        If File.Exists(strTarget) = False Then
            strReplaceMsg(0) = strTarget.Substring(strTarget.LastIndexOf("\") + 1)
            Me.ShowMessage("E315", strReplaceMsg)
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索条件変更チェックをします。
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsConditionChangedCheck(ByVal strKeyTypeUsed As String, ByVal strKeyNoUsed As String, _
                                            ByVal strKeyTypeNow As String, ByVal strKeyNoNow As String, _
                                            ByVal strKeyTypeUsedNM As String, ByVal strRplcMsg1 As String) As Boolean

        If (String.IsNullOrEmpty(strKeyTypeUsed) = False AndAlso _
            String.IsNullOrEmpty(strKeyNoNow) = False) AndAlso _
           (strKeyTypeUsed.Equals(strKeyTypeNow) = False OrElse _
            strKeyNoUsed.Equals(strKeyNoNow) = False) Then
            Dim strReplaceMsg(3) As String
            strReplaceMsg(0) = strRplcMsg1
            strReplaceMsg(1) = strKeyTypeUsedNM
            strReplaceMsg(2) = strKeyNoUsed
            Me.ShowMessage("E314", strReplaceMsg)
            Return False
        End If

        Return True

    End Function

#Region "内部メソッド"

    ''' <summary>
    '''入力チェック(Run押下時)
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Private Function IsInputChk() As Boolean

        With Me._Frm

            'SYSTEM_ID
            .cmbSystemID.ItemName = .lblSystemID.Text
            .cmbSystemID.IsHissuCheck = True
            If Me.IsValidateCheck(.cmbSystemID) = False Then
                Return False
            End If

            'KEY_TYPE
            .cmbKeyType.ItemName = .lblKeyType.Text
            .cmbKeyType.IsHissuCheck = True
            If Me.IsValidateCheck(.cmbKeyType) = False Then
                Return False
            End If

            'KEY_NO
            Dim keyType As String = .cmbKeyType.SelectedValue.ToString()
            .txtKeyNo.ItemName = .lblKeyNo.Text
            .txtKeyNo.IsHissuCheck = True
            .txtKeyNo.IsForbiddenWordsCheck = True

        End With

        Return True

    End Function

    ''' <summary>
    ''' 英大数チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHankakuEisuCheck() As Boolean

        With Me._Frm

            '画面の値を英大数コントロールに設定
            .txtChkControl.TextValue = .txtKeyNo.TextValue

            '' ''桁数が違う場合、エラー
            ' ''If .txtChkControl.TextValue.Length <> .txtKeyNo.TextValue.Length Then
            ' ''    .txtKeyNo.BackColorDef = Utility.REGUIUtility.GetAttentionBackColor()
            ' ''    .txtKeyNo.Focus()
            ' ''    .txtKeyNo.Select()
            ' ''    MyBase.ShowMessage("E004", New String() {.lblKeyNo.Text})
            ' ''    Return False
            ' ''End If


        End With

        Return True

    End Function

#End Region

#End Region

End Class
