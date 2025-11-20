' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI180  : NRC出荷／回収情報入力
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports FarPoint.Win.Spread
Imports System.IO

''' <summary>
''' LMI180Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI180V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI180F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI180F, ByVal v As LMIControlV, ByVal g As LMI180G)

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
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI180C.EventShubetsu) As Boolean

        '【単項目チェック】
        With Me._Frm

            Dim modeFlg As String = String.Empty '01:出荷、02:回収、03:取消
            If .optShukka.Checked = True Then
                modeFlg = "01"
            ElseIf .optKaishu.Checked = True Then
                modeFlg = "02"
            ElseIf .optTorikeshi.Checked = True Then
                modeFlg = "03"
            End If

            If LMI180C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If ("01").Equals(modeFlg) = True OrElse _
                    ("03").Equals(modeFlg) = True Then
                    '出荷管理番号
                    .txtOutkaNoL.ItemName() = .lblTitleOutokaNoL.TextValue
                    .txtOutkaNoL.IsHissuCheck() = True
                    .txtOutkaNoL.IsForbiddenWordsCheck() = True
                    .txtOutkaNoL.IsByteCheck() = 9
                    If MyBase.IsValidateCheck(.txtOutkaNoL) = False Then
                        Return False
                    End If
                End If
            End If

            If LMI180C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If ("01").Equals(modeFlg) = True OrElse _
                    ("03").Equals(modeFlg) = True Then
                    'シリアル№　From
                    .txtSerialNoFrom.ItemName() = .lblTitleSerialNo.TextValue
                    .txtSerialNoFrom.IsForbiddenWordsCheck() = True
                    .txtSerialNoFrom.IsSujiCheck() = True
                    .txtSerialNoFrom.IsFullByteCheck() = 7
                    If MyBase.IsValidateCheck(.txtSerialNoFrom) = False Then
                        Return False
                    End If
                End If
            End If

            If LMI180C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If ("01").Equals(modeFlg) = True OrElse _
                    ("03").Equals(modeFlg) = True Then
                    'シリアル№　To
                    .txtSerialNoTo.ItemName() = .lblTitleSerialNo.TextValue
                    .txtSerialNoTo.IsForbiddenWordsCheck() = True
                    .txtSerialNoTo.IsSujiCheck() = True
                    .txtSerialNoTo.IsFullByteCheck() = 7
                    If MyBase.IsValidateCheck(.txtSerialNoTo) = False Then
                        Return False
                    End If
                End If
            End If

            If LMI180C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If ("02").Equals(modeFlg) = True  Then
                    '回収日
                    .imdKaishuDate.ItemName() = .lblTitleKaishuDate.TextValue
                    .imdKaishuDate.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.imdKaishuDate) = False Then
                        Return False
                    End If
                End If
            End If

            If LMI180C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                '報告日 From
                .imdHokokuDateFrom.ItemName() = .lblTitleHokokuDate.TextValue
                .imdHokokuDateFrom.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdHokokuDateFrom) = False Then
                    Return False
                End If
            End If

            If LMI180C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                '報告日 To
                .imdHokokuDateTo.ItemName() = .lblTitleHokokuDate.TextValue
                .imdHokokuDateTo.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdHokokuDateTo) = False Then
                    Return False
                End If
            End If


            '■一覧部のチェック
            Dim max As Integer = .sprDetails.ActiveSheet.Rows.Count - 1
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetails)

            If LMI180C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If ("02").Equals(modeFlg) = True  Then
                    '一覧
                    If max = -1 Then
                        MyBase.ShowMessage("E473", New String() {"一覧のシリアル№", String.Empty})
                        Return False
                    End If
                End If
            End If

            For i As Integer = 0 To max
                If LMI180C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                    If ("01").Equals(modeFlg) = True OrElse _
                        ("02").Equals(modeFlg) = True Then
                        'シリアル№
                        vCell.SetValidateCell(i, LMI180G.sprDetails.SERIALNO.ColNo)
                        vCell.ItemName() = "シリアル№"
                        vCell.IsHissuCheck() = True
                        vCell.IsForbiddenWordsCheck() = True
                        vCell.IsFullByteCheck() = 7
                        vCell.IsSujiCheck() = True
                        If MyBase.IsValidateCheck(vCell) = False Then
                            Return False
                        End If
                    End If
                End If
            Next

            '一覧内、シリアル№重複チェック
            Dim serialNoOld As String = String.Empty
            Dim serialNoNew As String = String.Empty
            Dim cnt As Integer = 0
            If LMI180C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If ("01").Equals(modeFlg) = True OrElse _
                    ("02").Equals(modeFlg) = True Then
                    For i As Integer = 0 To max
                        serialNoOld = Me._Vcon.GetCellValue(.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.SERIALNO.ColNo))
                        cnt = i + 1

                        For j As Integer = cnt To max
                            serialNoNew = Me._Vcon.GetCellValue(.sprDetails.ActiveSheet.Cells(j, LMI180G.sprDetails.SERIALNO.ColNo))
                            If (serialNoOld).Equals(serialNoNew) = True Then
                                MyBase.ShowMessage("E496", New String() {"シリアル№", String.Concat(i + 1, "行目と", j + 1, "行目")})
                                Return False
                            End If
                        Next

                    Next
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
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI180C.EventShubetsu) As Boolean

        '【関連項目チェック】
        With Me._Frm

            Dim modeFlg As String = String.Empty '01:出荷、02:回収、03:取消
            If .optShukka.Checked = True Then
                modeFlg = "01"
            ElseIf .optKaishu.Checked = True Then
                modeFlg = "02"
            ElseIf .optTorikeshi.Checked = True Then
                modeFlg = "03"
            End If

            If LMI180C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If ("01").Equals(modeFlg) = True OrElse _
                    ("03").Equals(modeFlg) = True Then
                    'シリアル№ From + To 両方の必須チェック
                    If (String.IsNullOrEmpty(.txtSerialNoFrom.TextValue) = True AndAlso _
                        String.IsNullOrEmpty(.txtSerialNoTo.TextValue) = False) OrElse _
                        (String.IsNullOrEmpty(.txtSerialNoFrom.TextValue) = False AndAlso _
                        String.IsNullOrEmpty(.txtSerialNoTo.TextValue) = True) Then
                        MyBase.ShowMessage("E224", New String() {"シリアル№", "FromとToの両方"})
                        .txtSerialNoFrom.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        .txtSerialNoTo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        Return False
                    End If
                End If
            End If

            If LMI180C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If ("01").Equals(modeFlg) = True OrElse _
                    ("03").Equals(modeFlg) = True Then
                    'シリアル№ From + To 大小チェック
                    If .txtSerialNoFrom.TextValue > .txtSerialNoTo.TextValue Then
                        MyBase.ShowMessage("E505", New String() {"シリアル№ To", "シリアル№ From"})
                        .txtSerialNoFrom.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        .txtSerialNoTo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        Return False
                    End If
                End If
            End If

            If LMI180C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                '報告日 大小チェック
                If .imdHokokuDateFrom.TextValue > .imdHokokuDateTo.TextValue Then
                    MyBase.ShowMessage("E039", New String() {"報告日To", "報告日From"})
                    .imdHokokuDateFrom.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    .imdHokokuDateTo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Return False
                End If
            End If

            If LMI180C.EventShubetsu.TORIKOMI.Equals(eventShubetsu) = True Then
                'ファイルの存在チェック
                '要望番号:1917 yamanaka 2013.03.06 Start
                'Dim folderNm As String = .lblFolder.TextValue
                'Dim fileNm As String = .lblFile.TextValue

                'If Me.IsExistFileChk(folderNm, fileNm) = False Then
                '    Return False
                'End If
                '要望番号:1917 yamanaka 2013.03.06 End
            End If


            '■一覧部のチェック
            Dim max As Integer = .sprDetails.ActiveSheet.Rows.Count - 1
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetails)
            'シリアル№の必須チェック
            If LMI180C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If ("01").Equals(modeFlg) = True Then
                    If max = -1 AndAlso _
                        String.IsNullOrEmpty(.txtSerialNoFrom.TextValue) = True AndAlso _
                        String.IsNullOrEmpty(.txtSerialNoTo.TextValue) = True Then
                        MyBase.ShowMessage("E473", New String() {"シリアル№", "シリアル№ From Toまたは一覧のシリアル№を入力してください。"})
                        Return False
                    End If
                End If
            End If


            'シリアル№のFrom～To範囲内チェック
            Dim serialNo As String = String.Empty
            If LMI180C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If ("01").Equals(modeFlg) = True  Then
                    For i As Integer = 0 To max
                        serialNo = Me._Vcon.GetCellValue(.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.SERIALNO.ColNo))
                        If .txtSerialNoFrom.TextValue <= serialNo AndAlso _
                            serialNo <= .txtSerialNoTo.TextValue Then
                            MyBase.ShowMessage("E502", New String() {String.Concat(i + 1, "行目")})
                            Return False
                        End If
                    Next
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 単項目入力チェック(CSV)（エラー）。
    ''' </summary>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCsvCheck(ByVal ds As DataSet) As DataSet

        '【単項目チェック】
        With Me._Frm

            Dim max As Integer = ds.Tables(LMI180C.TABLE_NM_IN).Rows.Count - 1
            For i As Integer = 0 To max

                .txtFileTextBox.TextValue = ds.Tables(LMI180C.TABLE_NM_IN).Rows(i).Item("SERIAL_NO").ToString
                .txtFileTextBox.IsNumericCheck() = True
                If MyBase.IsValidateCheck(.txtFileTextBox) = False Then
                    ds.Tables(LMI180C.TABLE_NM_IN).Rows(i).Item("TOROKU_KB") = "99"
                End If

            Next

        End With

        Return ds

    End Function

    ''' <summary>
    ''' ファイル存在チェック
    ''' </summary>
    ''' <param name="filePath">ファイルパス</param>
    ''' <param name="fileNm">ファイル名称</param>
    ''' <returns>True ：エラーなし False：エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsExistFileChk(ByVal filePath As String, _
                                   ByVal fileNm As String) As Boolean

        If String.IsNullOrEmpty(filePath) = True OrElse _
            String.IsNullOrEmpty(fileNm) = True Then
            MyBase.ShowMessage("E469", New String() {"取込対象のファイル"})
            Return False
        End If

        Dim fi As FileInfo = New FileInfo(String.Concat(filePath, fileNm))

        If fi.Exists = True Then
            Return True
        Else
            MyBase.ShowMessage("E469", New String() {"取込対象のファイル"})
            Return False
        End If

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI180C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI180C.EventShubetsu.TORIKOMI     '取込
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

            Case LMI180C.EventShubetsu.HOZON        '保存
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

            Case LMI180C.EventShubetsu.CLOSE        '閉じる
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

            Case LMI180C.EventShubetsu.EXCEL        'Excel出力
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

            Case LMI180C.EventShubetsu.ROWADD       '行追加
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

            Case LMI180C.EventShubetsu.ROWDEL       '行削除
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

    '(2013.03.07)要望番号1933 ファイル存在チェック -- START --
    ''' <summary>
    ''' ファイル存在チェック
    ''' </summary>
    ''' <param name="filePath_Name"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsFileExist(ByVal filePath_Name As String) As Boolean

        If System.IO.File.Exists(filePath_Name) = False Then

            'ファイルが存在しない場合、エラーにする
            Me.ShowMessage("E469", New String() {"取込対象のファイル"})
            Return False

        End If

        Return True

    End Function
    '(2013.03.07)要望番号1933 ファイル存在チェック --  END  --

#End Region 'Method

End Class
