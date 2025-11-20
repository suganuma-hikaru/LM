' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMU       : 文書管理
'  プログラムID     :  LMU020V   : スキャン取込
'  作  成  者       :  NRS)OHNO
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports System.IO
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMU020Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' 2006/07/21 IWAMOTO
''' </histry>
Public Class LMU020V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMU020F

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMU020F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck(ByRef frm As LMU020F) As Boolean

        'With frm

        '    Dim vCell As NFValidatableCells = New NFValidatableCells(.sprComp)

        '    vCell.SetValidateCell(0, 2)
        '    vCell.ItemName = "会社コード"
        '    vCell.IsForbiddenWordsCheck = True
        '    vCell.IsByteCheck = 6

        '    If Me.IsValidateCheck(vCell) = False Then Return False

        '    vCell.SetValidateCell(0, 3)
        '    vCell.ItemName = "円JDEコード"
        '    vCell.IsForbiddenWordsCheck = True
        '    vCell.IsMiddleSpace = True
        '    vCell.IsByteCheck = 8

        '    If Me.IsValidateCheck(vCell) = False Then Return False

        '    vCell.SetValidateCell(0, 4)
        '    vCell.ItemName = "会社名"
        '    vCell.IsForbiddenWordsCheck = True
        '    vCell.IsByteCheck = 50

        '    If Me.IsValidateCheck(vCell) = False Then Return False

        '    vCell.SetValidateCell(0, 5)
        '    vCell.ItemName = "部署名"
        '    vCell.IsForbiddenWordsCheck = True
        '    vCell.IsByteCheck = 50

        '    If Me.IsValidateCheck(vCell) = False Then Return False

        '    vCell.SetValidateCell(0, 7)
        '    vCell.ItemName = "業務担当者"
        '    vCell.IsForbiddenWordsCheck = True
        '    vCell.IsByteCheck = 20

        '    If Me.IsValidateCheck(vCell) = False Then Return False

        '    vCell.SetValidateCell(0, 9)
        '    vCell.ItemName = "営業担当者"
        '    vCell.IsForbiddenWordsCheck = True
        '    vCell.IsByteCheck = 20

        '    If Me.IsValidateCheck(vCell) = False Then Return False

        'End With

        Return True

    End Function

#End Region

#Region "フォルダ存在チェック"
   
    ''' <summary>
    ''' フォルダ存在チェック
    ''' </summary>
    ''' <param name="dirPath">フォルダパス</param>
    ''' <param name="actionType">処理区分</param>
    ''' <param name="errOutFlg">エラー表示有無 True：表示　False：非表示</param>
    ''' <returns>True：存在する　False：存在しない</returns>
    ''' <remarks></remarks>
    Friend Function IsDirectoryExistsCheck(ByVal dirPath As String, ByVal actionType As LMU020C.ActionType, ByVal errOutFlg As Boolean) As Boolean

        Dim rtnFlg As Boolean = True

        If System.IO.Directory.Exists(dirPath) = True Then
            'フォルダが存在する
            Return rtnFlg
        Else
            'フォルダが存在しない
            rtnFlg = False
        End If

        ' エラー表示なしの場合、処理終了
        If errOutFlg = False Then
            Return rtnFlg
        End If

        Select Case actionType

            Case LMU020C.ActionType.Scan
                'スキャンファイル取込 処理時チェック
                '20121026'Me.ShowMessage("E086")
                Me.ShowMessage("E371")
            Case LMU020C.ActionType.Open
                '取込結果フォルダOpen 処理時チェック
                '20121026'Me.ShowMessage("E087")
                Me.ShowMessage("E373")
            Case LMU020C.ActionType.JDEOut

            Case LMU020C.ActionType.Other

        End Select

        Return rtnFlg

    End Function

    ''' <summary>
    ''' ファイル存在チェック
    ''' </summary>
    ''' <param name="dirPath">フォルダパス</param>
    ''' <param name="actionType">処理区分</param>
    ''' <param name="errOutFlg">エラー表示有無 True：表示　False：非表示</param>
    ''' <param name="searchPtn">ファイル名</param>
    ''' <param name="searchOpt">SearchOption</param>
    ''' <returns>True：存在する　False：存在しない</returns>
    ''' <remarks></remarks>
    Friend Function IsFileExistsCheck(ByVal dirPath As String, _
                                      ByVal actionType As LMU020C.ActionType, _
                                      ByVal errOutFlg As Boolean, _
                                      ByVal searchPtn As String, _
                                      ByVal searchOpt As System.IO.SearchOption) As Boolean

        Dim rtnFlg As Boolean = True

        Dim files As String() = System.IO.Directory.GetFiles(dirPath, searchPtn, searchOpt)

        If files.Length > 0 Then
            'ファイルが存在する
            Return rtnFlg
        Else
            'ファイルが存在しない
            rtnFlg = False
        End If

        ' エラー表示なしの場合、処理終了
        If errOutFlg = False Then
            Return rtnFlg
        End If

        Select Case actionType

            Case LMU020C.ActionType.Scan

            Case LMU020C.ActionType.Open

            Case LMU020C.ActionType.JDEOut

            Case LMU020C.ActionType.Other

        End Select

        Return rtnFlg

    End Function

#End Region 'フォルダ存在チェック

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


#Region "ファイルロック状態のチェック"

    ''' <summary>
    ''' ファイルロック状態のチェック
    ''' </summary>
    ''' <param name="ckFilePath">チェック対象パス</param>
    ''' <returns>True：使用者なし　False：他のプロセスで使用中</returns>
    ''' <remarks></remarks>
    Friend Function ISUseingFileCheck(ByVal ckFilePath As String) As Boolean

        Dim rtnFlg As Boolean = True
        Dim reader As System.IO.FileStream = Nothing
        Try
            'チェック対象のファイルが存在するかチェック
            If System.IO.File.Exists(ckFilePath) Then
                'このプロセス以外からのアクセス禁止状態でファイルを開く
                reader = New System.IO.FileStream(ckFilePath, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.None)
            End If
        Catch lockex As IO.IOException
            '他のプロセスで利用していた場合は例外発生
            rtnFlg = False
            '20121026'Me.ShowMessage("E089", New String() {System.IO.Path.GetFileName(ckFilePath)})
            Me.ShowMessage("E375", New String() {System.IO.Path.GetFileName(ckFilePath)})
        Catch ex As Exception
            '他の要因での例外処理
            rtnFlg = False
            Me.ShowMessage("S002")   '予期せぬエラーが発生しました。システム管理者に連絡してください。
        Finally
            If Not reader Is Nothing Then
                reader.Close()
            End If
        End Try

        Return rtnFlg

    End Function

#End Region
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

            Case LMU020C.EVENT_IMPORT          'スキャン取込
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

            Case LMU020C.EVENT_FOLDEROPEN         '取込結果フォルダOpen
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

            Case LMU020C.EVENT_SAVE      '保存
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

            Case LMU020C.EVENT_CLOSE          '閉じる
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
End Class
