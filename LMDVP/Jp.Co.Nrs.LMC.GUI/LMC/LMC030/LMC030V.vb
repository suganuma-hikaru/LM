' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC030V : 送状番号入力
'  作  成  者       :  nishikawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC030Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMC030V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMC030F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMCControlV

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMC030F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = New LMCControlV(handlerClass, DirectCast(frm, Form))

        Me._ChkList = New ArrayList()

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "外部Method"

    ''' <summary>
    ''' 追加時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsTsuikaSingleCheck() As Boolean

        With Me._Frm

            Call Me.valueTrim()

            '【単項目チェック】
            '******************** ヘッダ項目の入力チェック ********************

            '出荷管理番号
            .txtSyukkaLNo.ItemName = "出荷管理番号"
            .txtSyukkaLNo.IsHissuCheck = True
            .txtSyukkaLNo.IsForbiddenWordsCheck = True
            .txtSyukkaLNo.IsFullByteCheck = 9
            If MyBase.IsValidateCheck(.txtSyukkaLNo) = False Then
                Return False
            End If

            '送り状番号
            .txtDenpNo.ItemName() = "送り状番号"
            .txtDenpNo.IsHissuCheck() = True
            .txtDenpNo.IsForbiddenWordsCheck() = True
            .txtDenpNo.IsByteCheck() = 20
            If MyBase.IsValidateCheck(.txtDenpNo) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 更新時関連チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsKoshinKanrenCheck() As Boolean

        With Me._Frm

            '【関連チェック】
            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            '※存在チェックはSV側で行う

        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存時関連チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsHozonKanrenCheck() As Boolean

        With Me._Frm

            '【関連チェック】
            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            '重複チェック
            If Me.IsDuplicationCheck() = False Then
                Return False
            End If

            '※存在チェックはSV側で行う

        End With

        Return True
    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMC030C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMC030C.EventShubetsu.TSUIKA          '追加
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

            Case LMC030C.EventShubetsu.KOUSHIN         '更新
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

            Case LMC030C.EventShubetsu.TORIKOMI        '取込
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

            Case LMC030C.EventShubetsu.HOZON          '保存
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

            Case LMC030C.EventShubetsu.CLOSE           '閉じる
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
        Dim defNo As Integer = LMC030C.SprColumnIndex.DEF

        Return Me._Vcon.SprSelectList(defNo, Me._Frm.sprOkuriList)

    End Function

#End Region '外部Method

#Region "内部Method"

    ''' <summary>
    ''' 選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSelectDataChk() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Me._ChkList = Me.getCheckList()

        '選択チェック
        If Me._Vcon.IsSelectChk(Me._ChkList.Count()) = False Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 重複チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsDuplicationCheck() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Me._ChkList = Me.getCheckList()

        'チェック行件数
        Dim max As Integer = Me._ChkList.Count - 1

        'チェック用連想配列
        Dim ht As Hashtable = New Hashtable()

        '出荷管理番号
        Dim outKaN As String = String.Empty

        'メッセージ
        Dim msg As String = String.Empty

        '行番号
        Dim rowNo As Integer = 0

        For i As Integer = 0 To max
            With Me._Frm.sprOkuriList
                rowNo = Convert.ToInt32(Me._ChkList(i))

                outKaN = .Sheets(0).Cells(rowNo, LMC030C.SprColumnIndex.OUTKA_CTL_NO).Value.ToString()

                If ht.ContainsKey(outKaN) = True Then

                    msg = "出荷管理番号" & outKaN.ToString()
                    'メッセージエリアの設定
                    MyBase.ShowMessage("E022", New String() {msg})
                    Return False

                Else

                    ht.Add(outKaN, String.Empty)

                End If

            End With
        Next

        Return True

    End Function

    ''' <summary>
    ''' 項目のTrim処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub valueTrim()

        With Me._Frm
            '各項目のTrim処理
            .txtSyukkaLNo.TextValue = Trim(.txtSyukkaLNo.TextValue)
            .txtDenpNo.TextValue = Trim(.txtDenpNo.TextValue)
        End With

    End Sub
#End Region '内部Method

#End Region 'Method

End Class
