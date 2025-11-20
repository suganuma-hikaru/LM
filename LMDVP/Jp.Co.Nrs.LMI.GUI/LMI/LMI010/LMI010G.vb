' ==========================================================================
'  システム名     : LM
'  サブシステム名 : LMA     : メニュー
'  プログラムID   : LMI010G : 荷主選択
'  作  成  者     : [笈川]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI010F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI010F, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMIConG = g
    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal rock As Boolean)

        Dim always As Boolean = True

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F9ButtonName = String.Empty
            .F10ButtonName = "選　択"
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = False
            .F10ButtonEnabled = rock
            .F11ButtonEnabled = False
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .CmbCustNm.TabIndex = 1
            .CmbShori.TabIndex = 2

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(ByVal rock As Boolean)

        With Me._Frm
            .CmbShori.Enabled = rock

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(Optional ByVal tmpKBN As String = "")

        With Me._Frm
            .CmbCustNm.Focus()

        End With

    End Sub

    ''' <summary>
    ''' 荷主名コンボボックスの選択肢表示制御
    ''' </summary>
    Friend Sub SetComboBoxCustNm()

        ' 区分M(KBN_GROUP_CD = 'N018' AND KBN_NM5 =《ログインユーザーの営業所コード》) キャッシュデータ取得
        ' レコード存在: 特定荷主機能開放レンタル先
        ' レコード不在: NRS営業所
        ' (※レコード不在という意味では、特定荷主機能使用不可レンタル先も該当するが、
        '  　特定荷主機能使用不可レンタル先は、メニューの制御により本機能を実行することはないため考慮しない)
        Dim whereKbnN018KbnNm5EqNrsBrCd As String =
                String.Concat(
                    "    KBN_GROUP_CD = 'N018'",
                    "AND KBN_NM5 = '", LM.Base.LMUserInfoManager.GetNrsBrCd().ToString(), "'",
                    "AND SYS_DEL_FLG = '0'")
        Dim drKbnN018KbnNm5EqNrsBrCd As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(whereKbnN018KbnNm5EqNrsBrCd.ToString())

        ' 区分M(KBN_GROUP_CD = 'N018') キャッシュデータ取得および Dictionary への格納
        Dim dictKbnN018 As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Dim drKbnN018 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'N018'")
        For Each dr As DataRow In drKbnN018
            ' キー: KBN_CD, 値: KBN_NM5(特定荷主機能開放レンタル先の場合: 営業所コード、それ以外の場合 "")
            dictKbnN018.Add(dr.Item("KBN_CD").ToString(), dr.Item("KBN_NM5").ToString())
        Next
        For i As Integer = 1 To Me._Frm.CmbCustNm.Items.Count - 1
            Dim kbnCd As String = Me._Frm.CmbCustNm.Items(i).SubItems(1).Value.ToString()
            Dim isVisible As Boolean
            If dictKbnN018.ContainsKey(kbnCd) Then
                ' 区分M(KBN_GROUP_CD = 'N018' AND KBN_CD =《コンボボックスの選択肢のコード》) 存在
                If drKbnN018KbnNm5EqNrsBrCd.Count = 0 Then
                    ' NRS営業所の場合
                    ' 「特定荷主機能開放レンタル先」以外の荷主の機能のみ表示する。
                    If dictKbnN018(kbnCd) = "" Then
                        isVisible = True
                    Else
                        isVisible = False
                    End If
                Else
                    ' 特定荷主機能開放レンタル先の場合
                    ' ログインユーザーの営業所コードと一致する
                    ' 「特定荷主機能開放レンタル先」荷主の機能のみ表示する。
                    If dictKbnN018(kbnCd) = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString() Then
                        isVisible = True
                    Else
                        isVisible = False
                    End If
                End If
            Else
                ' 区分M(KBN_GROUP_CD = 'N018' AND KBN_CD =《コンボボックスの選択肢のコード》) 不在
                ' 荷主名コンボボックスの元になったレコードのため、存在しないことは考えられないが、念のため。
                isVisible = False
            End If
            Me._Frm.CmbCustNm.Items(i).Visible = isVisible
        Next

    End Sub

    ''' <summary>
    ''' 画面名の設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetComboBoxShori(ByVal KbnCd As String)

        With Me._Frm

            'コンボボックスのクリア
            ClearComboBoxShori()

            Dim sql As String = String.Concat("KBN_GROUP_CD = 'N019' ", " AND KBN_NM2 = '", KbnCd, "' ")

            'コンボボックス生成
            Me._LMIConG.CreateComboBox(.CmbShori, LMConst.CacheTBL.KBN, New String() {"KBN_CD"}, New String() {"KBN_NM1"}, sql, "KBN_CD")

            'コンボボックスの設定
            Me.SetControlsStatus(True)

            'ファンクションキーの設定
            Me.SetFunctionKey(True)
        End With

    End Sub

    ''' <summary>
    ''' コンボボックスの画面名をクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearComboBoxShori()
        With Me._Frm

            .CmbShori.SelectedValue() = ""
            .CmbShori.Items.Clear()

        End With
    End Sub

#End Region

#End Region

#End Region

End Class
