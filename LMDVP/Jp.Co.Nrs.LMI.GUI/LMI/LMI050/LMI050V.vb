' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI050  : EDI月末在庫実績送信ﾃﾞｰﾀ作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI050Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI050V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI050F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI050F, ByVal v As LMIControlV, ByVal g As LMI050G)

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
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI050C.EventShubetsu) As Boolean

        '【単項目チェック】
        With Me._Frm

            If LMI050C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                '営業所
                .cmbEigyo.ItemName() = .lblTitleEigyo.Text
                .cmbEigyo.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                    Return False
                End If
            End If

            If LMI050C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                '荷主
                .cmbCust.ItemName() = .lblTitleCust.Text
                .cmbCust.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbCust) = False Then
                    Return False
                End If
            End If

            If LMI050C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                '実績日付
                .imdDate.ItemName() = .lblTitleDate.TextValue
                .imdDate.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdDate) = False Then
                    Return False
                End If
                If .imdDate.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {.lblTitleDate.TextValue, "8"})
                    Me._Vcon.SetErrorControl(.imdDate)
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
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI050C.EventShubetsu) As Boolean

        '【関連項目チェック】
        With Me._Frm
            Dim eventNm As String = String.Empty
            If LMI050C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                eventNm = LMI050C.EVENTNAME_JIKKO
            End If

            If LMI050C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                '出力先
                If .chkEDI.Checked = False AndAlso _
                    .chkExcel.Checked = False AndAlso _
                    .chkMail.Checked = False Then
                    MyBase.ShowMessage("E199", New String() {"出力先"})
                    Return False
                End If
            End If

            '区分マスタを取得し、区分マスタに設定されている値で設定する
            Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'E029' AND ", _
                                                                                                            "KBN_CD = '", .cmbCust.SelectedValue, "'"))
            Dim jissekiDate As String = .imdDate.TextValue
            If LMI050C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                '実績日付
                If kbnDr.Length > 0 Then
                    Select Case kbnDr(0).Item("KBN_NM7").ToString
                        Case LMI050C.DATE_FLG98, LMI050C.DATE_FLG99
                            'チェックなし

                        Case LMI050C.DATE_FLG00
                            '次の日を求める
                            Dim nextDate As String = Convert.ToString(DateSerial(Convert.ToInt32(Mid(jissekiDate, 1, 4)), Convert.ToInt32(Mid(jissekiDate, 5, 2)), Convert.ToInt32(Mid(jissekiDate, 7, 2))).AddDays(1))
                            If ("01").Equals(Mid(nextDate, 9, 2)) = False Then
                                '次の日が月始め以外の場合、エラー
                                MyBase.ShowMessage("E187", New String() {"月末制限の荷主", "月末"})
                                Me._Vcon.SetErrorControl(.imdDate)
                                Return False
                            End If

                        Case Else
                            If (kbnDr(0).Item("KBN_NM7").ToString).Equals(Mid(jissekiDate, 7, 2)) = False Then
                                'KBN_NM7に設定されている締め日区分と、実績日付の日の部分が異なる場合、エラー
                                MyBase.ShowMessage("E187", New String() {"締日制限の荷主", "締日"})
                                Me._Vcon.SetErrorControl(.imdDate)
                                Return False
                            End If
                    End Select
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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI050C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI050C.EventShubetsu.JIKKO        '実行
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

            Case LMI050C.EventShubetsu.CLOSE        '閉じる
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
