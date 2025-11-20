' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI070V : 請求データ作成 [ダウ・ケミカル用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI070Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI070V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI070F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI070F, ByVal v As LMIControlV, ByVal g As LMI070G)

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
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI070C.EventShubetsu) As Boolean

        '【単項目チェック】
        With Me._Frm

            If LMI070C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI070C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI070C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                '営業所
                .cmbEigyo.ItemName() = "営業所"
                .cmbEigyo.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                    Return False
                End If
            End If

            If LMI070C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI070C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI070C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                '荷主コード(大)
                .txtCustCdL.ItemName() = "荷主コード(大)"
                .txtCustCdL.IsHissuCheck() = True
                .txtCustCdL.IsForbiddenWordsCheck() = True
                .txtCustCdL.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    Return False
                End If
            End If

            If LMI070C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI070C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI070C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                '荷主コード(中)
                .txtCustCdM.ItemName() = "荷主コード(中)"
                .txtCustCdM.IsHissuCheck() = True
                .txtCustCdM.IsForbiddenWordsCheck() = True
                .txtCustCdM.IsFullByteCheck() = 2
                If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                    Return False
                End If
            End If

            If LMI070C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI070C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI070C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                '処理日付FROM
                .imdDateFrom.ItemName() = "処理日付From"
                .imdDateFrom.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdDateFrom) = False Then
                    Return False
                End If
                If .imdDateFrom.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {"処理日付From", "8"})
                    Return False
                End If
            End If

            If LMI070C.EventShubetsu.MAKE.Equals(eventShubetsu) = True Then
                '処理日付TO
                .imdDateTo.ItemName() = "処理日付To"
                .imdDateTo.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdDateTo) = False Then
                    Return False
                End If
                If .imdDateTo.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {"処理日付To", "8"})
                    Return False
                End If
            End If

            If LMI070C.EventShubetsu.MAKE.Equals(eventShubetsu) = True Then
                '作成項目
                .cmbMake.ItemName() = .lblTitleMake.Text
                .cmbMake.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbMake) = False Then
                    Return False
                End If
            End If

            If LMI070C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                '実行種別
                .cmbJikko.ItemName() = .lblTitleJikko.Text
                .cmbJikko.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbJikko) = False Then
                    Return False
                End If
            End If

            If LMI070C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                '印刷種別
                .cmbPrint1.ItemName() = .lblTitlePrint.Text
                .cmbPrint1.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbPrint1) = False Then
                    Return False
                End If
            End If

            If LMI070C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                '印刷種別
                .cmbPrint2.ItemName() = .lblTitlePrint.Text
                .cmbPrint2.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbPrint2) = False Then
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
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI070C.EventShubetsu) As Boolean


        '【関連項目チェック】
        With Me._Frm
            Dim eventNm As String = String.Empty
            If LMI070C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                eventNm = LMI070C.EVENTNAME_JIKKO
            ElseIf LMI070C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
                eventNm = LMI070C.EVENTNAME_MASTER
            ElseIf LMI070C.EventShubetsu.CLOSE.Equals(eventShubetsu) = True Then
                eventNm = LMI070C.EVENTNAME_CLOSE
            ElseIf LMI070C.EventShubetsu.MAKE.Equals(eventShubetsu) = True Then
                eventNm = LMI070C.EVENTNAME_MAKE
            ElseIf LMI070C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                eventNm = LMI070C.EVENTNAME_PRINT
            End If

            If LMI070C.EventShubetsu.MAKE.Equals(eventShubetsu) = True Then
                If .imdDateTo.TextValue < .imdDateFrom.TextValue Then
                    '処理日付FROM ＋ 処理日付TO
                    MyBase.ShowMessage("E166", New String() {"処理日付To", "処理日付From"})
                    .imdDateTo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.imdDateFrom)
                    Return False
                Else
                    .imdDateFrom.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    .imdDateTo.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                End If
            End If

            If LMI070C.EventShubetsu.MAKE.Equals(eventShubetsu) = True Then
                '処理日付TOの月末日付を求める
                Dim lastDate As String = Convert.ToString(Date.DaysInMonth(Convert.ToInt32(Mid(.imdDateTo.TextValue, 1, 4)), Convert.ToInt32(Mid(.imdDateTo.TextValue, 5, 2))))
                If (lastDate).Equals(Mid(.imdDateTo.TextValue, 7, 2)) = False Then
                    '処理日付TOの月末チェック
                    If MyBase.ShowMessage("W191", New String() {"処理日付To", "月末日"}) = MsgBoxResult.Cancel Then
                        Me._Vcon.SetErrorControl(.imdDateTo)
                        Return False
                    Else
                        .imdDateTo.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    End If
                End If
            End If

            Dim custDetailsDr As DataRow() = Nothing
            If LMI070C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI070C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI070C.EventShubetsu.JIKKO.Equals(eventShubetsu) = True Then
                '要望番号:1253 terakawa 2012.07.13 Start
                'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD LIKE '", .txtCustCdL.TextValue, "%' AND SUB_KB = '23' AND SET_NAIYO = '01'"))
                custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, _
                                                                                                                "' AND CUST_CD LIKE '", .txtCustCdL.TextValue, _
                                                                                                                "%' AND SUB_KB = '23' AND SET_NAIYO = '01'"))
                '要望番号:1253 terakawa 2012.07.13 End
                If custDetailsDr.Length = 0 Then
                    '荷主明細マスタ存在チェック
                    MyBase.ShowMessage("E464", New String() {eventNm})
                    Return False
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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI070C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI070C.EventShubetsu.MASTER       'マスタ参照
                '10:閲覧者の場合エラー
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

            Case LMI070C.EventShubetsu.MAKE         '作成
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

            Case LMI070C.EventShubetsu.JIKKO        'Excel出力
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

            Case LMI070C.EventShubetsu.PRINT        '印刷
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

            Case LMI070C.EventShubetsu.CLOSE        '閉じる
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
