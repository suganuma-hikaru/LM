' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI030V : 請求データ作成 [デュポン用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI030Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI030V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI030F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI030F, ByVal v As LMIControlV, ByVal g As LMI030G)

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
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI030C.EventShubetsu) As Boolean

        '【単項目チェック】
        With Me._Frm

            'START YANAI 要望番号830
            'If LMI030C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
            '    LMI030C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True OrElse _
            '    LMI030C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
            '    LMI030C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
            If LMI030C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                'END YANAI 要望番号830
                '営業所
                .cmbEigyo.ItemName() = "営業所"
                .cmbEigyo.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                    Return False
                End If
            End If

            If LMI030C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                '荷主コード(大)
                .txtCustCdL.ItemName() = "荷主コード(大)"
                .txtCustCdL.IsHissuCheck() = True
                .txtCustCdL.IsForbiddenWordsCheck() = True
                .txtCustCdL.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    Return False
                End If
            End If

            If LMI030C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                '荷主コード(中)
                .txtCustCdM.ItemName() = "荷主コード(中)"
                If LMI030C.EventShubetsu.MAKE.Equals(eventShubetsu) = False Then
                    .txtCustCdM.IsHissuCheck() = True
                End If
                .txtCustCdM.IsForbiddenWordsCheck() = True
                .txtCustCdM.IsFullByteCheck() = 2
                If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                    Return False
                End If
            End If

            If LMI030C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                '荷主コード(小)
                .txtCustCdS.ItemName() = "荷主コード(小)"
                .txtCustCdS.IsForbiddenWordsCheck() = True
                .txtCustCdS.IsFullByteCheck() = 2
                If MyBase.IsValidateCheck(.txtCustCdS) = False Then
                    Return False
                End If
            End If

            If LMI030C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                '荷主コード(極小)
                .txtCustCdSS.ItemName() = "荷主コード(極小)"
                .txtCustCdSS.IsForbiddenWordsCheck() = True
                .txtCustCdSS.IsFullByteCheck() = 2
                If MyBase.IsValidateCheck(.txtCustCdSS) = False Then
                    Return False
                End If
            End If

            If LMI030C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                '請求月FROM
                .imdSeiqtoFrom.ItemName() = "請求月From"
                .imdSeiqtoFrom.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdSeiqtoFrom) = False Then
                    Return False
                End If
                If .imdSeiqtoFrom.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {"請求月From", "8"})
                    Return False
                End If
            End If

            If LMI030C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                '請求月TO
                .imdSeiqtoTo.ItemName() = "請求月To"
                .imdSeiqtoTo.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdSeiqtoTo) = False Then
                    Return False
                End If
                If .imdSeiqtoTo.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {"請求月To", "8"})
                    Return False
                End If
            End If

            If LMI030C.EventShubetsu.MAKE.Equals(eventShubetsu) = True Then
                '請求項目
                .cmbSeikyu.ItemName() = .lblTitleSeikyu.Text
                .cmbSeikyu.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbSeikyu) = False Then
                    Return False
                End If
            End If

            If LMI030C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
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
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI030C.EventShubetsu) As Boolean


        '【関連項目チェック】
        With Me._Frm
            Dim eventNm As String = String.Empty
            If LMI030C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                eventNm = LMI030C.EVENTNAME_EXCEL
            ElseIf LMI030C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                eventNm = LMI030C.EVENTNAME_KENSAKU
            ElseIf LMI030C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
                eventNm = LMI030C.EVENTNAME_MASTER
            ElseIf LMI030C.EventShubetsu.CLOSE.Equals(eventShubetsu) = True Then
                eventNm = LMI030C.EVENTNAME_CLOSE
            ElseIf LMI030C.EventShubetsu.MAKE.Equals(eventShubetsu) = True Then
                eventNm = LMI030C.EVENTNAME_MAKE
            ElseIf LMI030C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                eventNm = LMI030C.EVENTNAME_PRINT
            End If

            If LMI030C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI030C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                If .imdSeiqtoTo.TextValue < .imdSeiqtoFrom.TextValue Then
                    '請求月FROM ＋ 請求月TO
                    MyBase.ShowMessage("E166", New String() {"請求月To", "請求月From"})
                    .imdSeiqtoTo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.imdSeiqtoFrom)
                    Return False
                Else
                    .imdSeiqtoFrom.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    .imdSeiqtoTo.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                End If
            End If

            If LMI030C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
                '請求月TOの月末日付を求める
                Dim lastDate As String = Convert.ToString(Date.DaysInMonth(Convert.ToInt32(Mid(.imdSeiqtoTo.TextValue, 1, 4)), Convert.ToInt32(Mid(.imdSeiqtoTo.TextValue, 5, 2))))
                If (lastDate).Equals(Mid(.imdSeiqtoTo.TextValue, 7, 2)) = False Then
                    '請求月TOの月末チェック
                    If MyBase.ShowMessage("W191", New String() {"請求月To", "月末日"}) = MsgBoxResult.Cancel Then
                        Me._Vcon.SetErrorControl(.imdSeiqtoTo)
                        Return False
                    Else
                        .imdSeiqtoTo.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    End If
                End If
            End If

            'START YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない
            'Dim custDetailsDr As DataRow() = Nothing
            'If LMI030C.EventShubetsu.MAKE.Equals(eventShubetsu) = True OrElse _
            '    LMI030C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True OrElse _
            '    LMI030C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
            '    LMI030C.EventShubetsu.EXCEL.Equals(eventShubetsu) = True Then
            '    '要望番号:1253 terakawa 2012.07.13 Start
            '    'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD LIKE '", .txtCustCdL.TextValue, "%' AND SUB_KB = '01'"))
            '    custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND CUST_CD LIKE '", .txtCustCdL.TextValue, "%' AND SUB_KB = '01'"))
            '    '要望番号:1253 terakawa 2012.07.13 End
            '    If custDetailsDr.Length = 0 Then
            '        '荷主明細マスタ存在チェック
            '        MyBase.ShowMessage("E431", New String() {eventNm})
            '        Return False
            '    End If
            'End If
            'END YANAI 要望番号1349 デュポンデータ作成で大阪分が作成できない

        End With

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI030C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI030C.EventShubetsu.EXCEL        'Excel出力
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

            Case LMI030C.EventShubetsu.MASTER       'マスタ参照
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

            Case LMI030C.EventShubetsu.MAKE         '作成
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

            Case LMI030C.EventShubetsu.PRINT        '印刷
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

            Case LMI030C.EventShubetsu.KENSAKU      '検索
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

            Case LMI030C.EventShubetsu.CLOSE        '閉じる
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
