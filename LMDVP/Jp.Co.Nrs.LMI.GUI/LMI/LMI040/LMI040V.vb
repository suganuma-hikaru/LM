' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求
'  プログラムID     :  LMI040V : 請求データ編集[デュポン用]
'  作  成  者       :  [HISHI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI040Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI040V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI040F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI040F, ByVal v As LMIControlV, ByVal g As LMI040G)

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
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI040C.EventShubetsu, ByVal arr As ArrayList) As Boolean

        '【単項目チェック】
        With Me._Frm

            Dim kbnDr() As DataRow = Nothing

            'スペース除去
            Call Me.TrimSpaceTextValue()

            'マイナス０を変換
            Call Me.ZeroTextValue()

            '【検索項目部】
            'START YANAI 要望番号830
            'If LMI040C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True OrElse _
            '    LMI040C.EventShubetsu.FILEMAKE.Equals(eventShubetsu) = True OrElse _
            '    LMI040C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
            If LMI040C.EventShubetsu.FILEMAKE.Equals(eventShubetsu) = True OrElse _
                LMI040C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                'END YANAI 要望番号830
                '営業所
                .cmbEigyo.ItemName() = .lblTitleEigyo.TextValue
                .cmbEigyo.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                    Return False
                End If
            End If

            If LMI040C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True OrElse _
                LMI040C.EventShubetsu.FILEMAKE.Equals(eventShubetsu) = True OrElse _
                LMI040C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                '請求月
                .imdSeikyu.ItemName() = .lblTitleSeikyuKikan.TextValue
                .imdSeikyu.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdSeikyu) = False Then
                    Return False
                End If
            End If

            If LMI040C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                '印刷種別
                .cmbPrint.ItemName() = .lblTitlePrint.TextValue
                .cmbPrint.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbPrint) = False Then
                    Return False
                End If
            End If

            'Add20120925 要望管理1430
            If LMI040C.EventShubetsu.FILEMAKE.Equals(eventShubetsu) = True OrElse _
               (LMI040C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso LMI040C.PRINT_SEKY_KAGAMI.Equals(Me._Frm.cmbPrint.SelectedValue.ToString()) = True) Then
                '請求先
                .cmbSEIQTO_KBN.ItemName() = .lblTitleSeiqto.TextValue
                .cmbSEIQTO_KBN.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbSEIQTO_KBN) = False Then
                    Return False
                End If
            End If

            If LMI040C.EventShubetsu.FILEMAKE.Equals(eventShubetsu) = True Then
                'ファイル作成種別
                .cmbFile.ItemName() = .lblTileFile.TextValue
                .cmbFile.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbFile) = False Then
                    Return False
                End If
            End If


            '【スプレッド部】
            If LMI040C.EventShubetsu.DEL.Equals(eventShubetsu) = True OrElse _
                (LMI040C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso LMI040C.PRINT_CHECK_LIST.Equals(Me._Frm.cmbPrint.SelectedValue.ToString()) = True) Then
                '選択チェック
                If Me.IsSelectDataChk(arr) = False Then
                    Return False
                End If
            End If


            '【編集項目部】
            If LMI040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '営業所
                If .cmbEigyo2.ReadOnly = False Then
                    .cmbEigyo2.ItemName() = .lblTitleEigyo2.TextValue
                    .cmbEigyo2.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.cmbEigyo2) = False Then
                        Return False
                    End If
                End If
            End If

            If LMI040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '請求月
                If .imdTuki.ReadOnly = False Then
                    .imdTuki.ItemName() = .lblTitleSikyuTuki.TextValue
                    .imdTuki.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.imdTuki) = False Then
                        Return False
                    End If
                End If
            End If

            If LMI040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '事業部
                If .cmbJigyou.ReadOnly = False Then
                    .cmbJigyou.ItemName() = .lblTitleJigyou.TextValue
                    .cmbJigyou.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.cmbJigyou) = False Then
                        Return False
                    End If
                End If
            End If

            If LMI040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '請求項目
                If .cmbSeikyukoumoku2.ReadOnly = False Then
                    .cmbSeikyukoumoku2.ItemName() = .lblTitleSeikyuKomoku2.TextValue
                    .cmbSeikyukoumoku2.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.cmbSeikyukoumoku2) = False Then
                        Return False
                    End If
                End If
            End If

            If LMI040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                'FRBコード
                If .txtFrbcd.ReadOnly = False Then
                    .txtFrbcd.ItemName() = .lblTitleFrbcd.TextValue
                    .txtFrbcd.IsForbiddenWordsCheck() = True
                    .txtFrbcd.IsFullByteCheck() = 7
                    If MyBase.IsValidateCheck(.txtFrbcd) = False Then
                        Return False
                    End If
                End If
            End If

            If LMI040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                'SRCコード
                If .txtSrc.ReadOnly = False Then
                    .txtSrc.ItemName() = .lblTitleSrcCd.TextValue
                    .txtSrc.IsForbiddenWordsCheck() = True
                    .txtSrc.IsFullByteCheck() = 2
                    If MyBase.IsValidateCheck(.txtSrc) = False Then
                        Return False
                    End If

                    If String.IsNullOrEmpty(.txtSrc.TextValue) = False Then
                        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'S028' AND ", _
                                                                                                       "KBN_CD = '", .txtSrc.TextValue, "'"))
                        If kbnDr.Length = 0 Then
                            MyBase.ShowMessage("E079", New String() {"区分マスタ", .lblTitleSrcCd.TextValue})
                            Me._Vcon.SetErrorControl(.txtSrc)
                            Return False
                        End If
                    End If
                End If

            End If

            If LMI040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                'コストセンター
                If .txtCust.ReadOnly = False Then
                    .txtCust.ItemName() = .lblTitleCust.TextValue
                    .txtCust.IsForbiddenWordsCheck() = True
                    .txtCust.IsFullByteCheck() = 10
                    If MyBase.IsValidateCheck(.txtCust) = False Then
                        Return False
                    End If
                End If
            End If

            If LMI040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '向国コード
                If .txtDestCity.ReadOnly = False Then
                    .txtDestCity.ItemName() = .lblTitleDestCity.TextValue
                    .txtDestCity.IsForbiddenWordsCheck() = True
                    .txtDestCity.IsByteCheck() = 3
                    If MyBase.IsValidateCheck(.txtDestCity) = False Then
                        Return False
                    End If
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連項目入力チェック（エラー）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI040C.EventShubetsu) As Boolean


        '【関連項目チェック】
        With Me._Frm
            
            If LMI040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If String.IsNullOrEmpty(.txtCust.TextValue) = False AndAlso _
                    (String.IsNullOrEmpty(.txtFrbcd.TextValue) = False OrElse _
                     String.IsNullOrEmpty(.txtSrc.TextValue) = False) AndAlso _
                     .txtCust.ReadOnly = False AndAlso _
                     .txtFrbcd.ReadOnly = False AndAlso _
                     .txtSrc.ReadOnly = False Then
                    'コストセンター ＋ FRBコード ＋ SRCコード
                    MyBase.ShowMessage("E448", New String() {.lblTitleCust.TextValue, String.Concat(.lblTitleFrbcd.TextValue, "・", .lblTitleSrcCd.TextValue)})
                    .txtCust.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    .txtSrc.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.txtFrbcd)
                    Return False
                ElseIf .txtCust.ReadOnly = False AndAlso _
                        .txtFrbcd.ReadOnly = False AndAlso _
                        .txtSrc.ReadOnly = False Then
                    .txtFrbcd.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    .txtSrc.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    .txtCust.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                End If
            End If

            If LMI040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If ("04").Equals(.cmbSeikyukoumoku2.SelectedValue) = False AndAlso _
                    String.IsNullOrEmpty(.txtDestCity.TextValue) = False AndAlso _
                    (.cmbSeikyukoumoku2.ReadOnly = False OrElse _
                    .txtDestCity.ReadOnly = False) Then

                    '請求項目 ＋ 向国コード
                    MyBase.ShowMessage("E449")
                    .cmbSeikyukoumoku2.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.txtDestCity)
                    Return False
                Else
                    If .cmbSeikyukoumoku2.ReadOnly = False  Then
                        .cmbSeikyukoumoku2.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    End If
                    If .txtDestCity.ReadOnly = False Then
                        .txtDestCity.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    End If
                End If
            End If

            If LMI040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If 0 < Convert.ToDecimal(.numKazei.TextValue) AndAlso Convert.ToDecimal(.numZeigaku.TextValue) = 0 Then
                    '課税金額 ＋ 税額
                    MyBase.ShowMessage("E187", New String() {"課税金額が0円以上", "税額を0円以上"})
                    .numZeigaku.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.numKazei)
                    Return False
                Else
                    .numKazei.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    .numZeigaku.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                End If
            End If

            If LMI040C.EventShubetsu.SEIKYUKEISAN.Equals(eventShubetsu) = True OrElse _
                LMI040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                Dim skyuKingaku As Decimal = Convert.ToDecimal(.numHiKazei.Value) + Convert.ToDecimal(.numKazei.Value)
                If skyuKingaku > Convert.ToDecimal(LMI040C.KINGAKU_MAX) AndAlso _
                    .numHiKazei.ReadOnly = False AndAlso _
                    .numKazei.ReadOnly = False Then
                    '非課税金額 ＋ 課税金額の桁数チェック
                    MyBase.ShowMessage("E002", New String() {"課税金額と非課税金額を加算した結果"})
                    .numHiKazei.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    .numKazei.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    If Convert.ToDecimal(.numKazei.Value) >= Convert.ToDecimal(.numHiKazei.Value) Then
                        Me._Vcon.SetErrorControl(.numKazei)
                    Else
                        Me._Vcon.SetErrorControl(.numHiKazei)
                    End If
                    Return False
                ElseIf .numHiKazei.ReadOnly = False AndAlso _
                        .numKazei.ReadOnly = False Then
                    .numKazei.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    .numHiKazei.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                End If
            End If

            'START YANAI 要望番号830
            If LMI040C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If Convert.ToDecimal(.numHiKazei.Value) = 0 AndAlso _
                    Convert.ToDecimal(.numKazei.Value) = 0 AndAlso _
                    Convert.ToDecimal(.numZeigaku.Value) = 0 Then
                    '非課税金額 ＋ 課税金額 ＋ 税額の金額チェック
                    MyBase.ShowMessage("E466")
                    .numHiKazei.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    .numZeigaku.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.numKazei)
                    Return False
                Else
                    .numKazei.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    .numHiKazei.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    .numZeigaku.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                End If
            End If

            'END YANAI 要望番号830

        End With

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI040C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI040C.EventShubetsu.PRINT        '印刷
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

            Case LMI040C.EventShubetsu.KENSAKU      '検索
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

            Case LMI040C.EventShubetsu.CLOSE        '閉じる
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

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMI040C.SprColumnIndex.DEF

        Return Me._Vcon.SprSelectList2(defNo, Me._Frm.sprDetail)

    End Function

    ''' <summary>
    ''' 選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectDataChk(ByVal arr As ArrayList) As Boolean

        '選択チェック
        If Me._Vcon.IsSelectChk(arr.Count) = False Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 項目のTrim処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceTextValue()

        With Me._Frm
            '各項目のTrim処理
            .txtFrbcd.TextValue = Trim(.txtFrbcd.TextValue)
            .txtSrc.TextValue = Trim(.txtSrc.TextValue)
            .txtCust.TextValue = Trim(.txtCust.TextValue)
        End With

    End Sub

    ''' <summary>
    ''' マイナス０を変換
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ZeroTextValue()

        With Me._Frm
            '各項目のTrim処理
            '.numKazei.Value = System.Math.Abs(Convert.ToDecimal(.numKazei.Value))
            '.numHiKazei.Value = System.Math.Abs(Convert.ToDecimal(.numHiKazei.Value))
            '.numZeigaku.Value = System.Math.Abs(Convert.ToDecimal(.numZeigaku.Value))
        End With

    End Sub

#End Region 'Method

End Class
