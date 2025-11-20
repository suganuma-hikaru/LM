' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME030  : 作業指示書検索
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LME030Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LME030V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LME030F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMEControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LME030F, ByVal v As LMEControlV, ByVal g As LME030G)

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
    Friend Function IsSingleCheck(ByVal eventShubetsu As LME030C.EventShubetsu) As Boolean

        '【単項目チェック】
        With Me._Frm

            'START YANAI 要望番号1090 指摘修正
            If LME030C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '作業日FROM
                '2016.01.06 UMANO 英語化対応START
                '.imdDateFrom.ItemName() = "作業日FROM"
                .imdDateFrom.ItemName() = String.Concat(.lblTitleDate.Text, " From")
                '2016.01.06 UMANO 英語化対応END
                If .imdDateFrom.IsDateFullByteCheck(8) = False Then
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E038", New String() {"作業日FROM", "8"})
                    MyBase.ShowMessage("E038", New String() {.imdDateFrom.ItemName(), "8"})
                    '2016.01.06 UMANO 英語化対応END
                    Return False
                End If
            End If

            If LME030C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '作業日TO
                '2016.01.06 UMANO 英語化対応START
                '.imdDateTo.ItemName() = "作業日TO"
                .imdDateTo.ItemName() = String.Concat(.lblTitleDate.Text, " To")
                '2016.01.06 UMANO 英語化対応END
                If .imdDateTo.IsDateFullByteCheck(8) = False Then
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E038", New String() {"作業日TO", "8"})
                    MyBase.ShowMessage("E038", New String() {.imdDateTo.ItemName(), "8"})
                    '2016.01.06 UMANO 英語化対応END
                    Return False
                End If
            End If
            'END YANAI 要望番号1090 指摘修正

            If LME030C.EventShubetsu.SINKI.Equals(eventShubetsu) = True OrElse _
                LME030C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True OrElse _
                LME030C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
                '荷主コード(大)
                '2016.01.06 UMANO 英語化対応START
                '.txtCustCdL.ItemName() = "荷主コード(大)"
                .txtCustCdL.ItemName() = .lblTitleCust.Text()
                '2016.01.06 UMANO 英語化対応END
                .txtCustCdL.IsForbiddenWordsCheck() = True
                .txtCustCdL.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    Return False
                End If
            End If

            If LME030C.EventShubetsu.SINKI.Equals(eventShubetsu) = True OrElse _
                LME030C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True OrElse _
                LME030C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
                '荷主コード(中)
                '2016.01.06 UMANO 英語化対応START
                '.txtCustCdM.ItemName() = "荷主コード(中)"
                .txtCustCdM.ItemName() = .lblTitleCust.Text()
                '2016.01.06 UMANO 英語化対応END
                .txtCustCdM.IsForbiddenWordsCheck() = True
                .txtCustCdM.IsFullByteCheck() = 2
                If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                    Return False
                End If
            End If


            '******************** スプレッド項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetails)

            '作業指示書番号
            vCell.SetValidateCell(0, LME030G.sprDetailsDef.SAGYOSIJINO.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName() = "作業指示書番号"
            vCell.ItemName() = LME030G.sprDetailsDef.SAGYOSIJINO.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名
            vCell.SetValidateCell(0, LME030G.sprDetailsDef.CUSTNM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName() = "荷主名"
            vCell.ItemName() = LME030G.sprDetailsDef.CUSTNM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品名
            vCell.SetValidateCell(0, LME030G.sprDetailsDef.GOODSNM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName() = "商品名"
            vCell.ItemName() = LME030G.sprDetailsDef.GOODSNM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '作業内容
            vCell.SetValidateCell(0, LME030G.sprDetailsDef.SAGYONM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName() = "作業内容"
            vCell.ItemName() = LME030G.sprDetailsDef.SAGYONM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
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
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LME030C.EventShubetsu) As Boolean


        '【関連項目チェック】
        With Me._Frm

            If LME030C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                If .imdDateTo.TextValue < .imdDateFrom.TextValue AndAlso _
                    String.IsNullOrEmpty(.imdDateFrom.TextValue) = False AndAlso _
                    String.IsNullOrEmpty(.imdDateTo.TextValue) = False Then
                    '作業日FROM ＋ 作業日TO
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E166", New String() {"作業日To", "作業日From"})
                    MyBase.ShowMessage("E166", New String() {String.Concat(.lblTitleDate.Text, " To"), String.Concat(.lblTitleDate.Text, " From")})
                    '2016.01.06 UMANO 英語化対応END
                    .imdDateTo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.imdDateFrom)
                    Return False
                Else
                    .imdDateFrom.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    .imdDateTo.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LME030C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LME030C.EventShubetsu.SINKI        '新規
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

            Case LME030C.EventShubetsu.KENSAKU      '検索
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

            Case LME030C.EventShubetsu.MASTER       'マスタ参照
                '10:閲覧者の場合エラー
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


            Case LME030C.EventShubetsu.CLOSE        '閉じる
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

            Case LME030C.EventShubetsu.COMPLETE      '完了
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

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LME030C.SprColumnIndex.DEF

        Return Me._Vcon.SprSelectList(defNo, Me._Frm.sprDetails)

    End Function


    ''' <summary>
    ''' 実行時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsJikkouInputCheck(ByVal arr As ArrayList) As Boolean

        Dim lgm As New Jp.Co.Nrs.LM.Utility.lmLangMGR(Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage)

        With Me._Frm

            '【単項目チェック】
            .cmbJikkou.ItemName() = lgm.Selector({"実行種別", "Execution type", "실행종별", "中国語"})
            .cmbJikkou.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbJikkou) = False Then
                Return False
            End If


        End With

        Return True

    End Function

#Region "完了時チェック"
    ''' <summary>
    ''' 完了時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanryoSingleCheck(ByVal arr As ArrayList, ByVal g As LME030G) As Boolean

        With Me._Frm
            '【単項目チェック】


            'スプレッド項目の入力チェック
            Dim max As Integer = arr.Count - 1
            Dim chkVal As String = String.Empty
            Dim errVal As Integer = 0

            '選択チェック
            If Me._Vcon.IsSelectChk(arr.Count()) = False Then
                MyBase.ShowMessage("E009")
                Return False
            End If

            '完了チェック
            For i As Integer = 0 To max
                chkVal = .sprDetails.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LME030G.sprDetailsDef.SAGYOSIJISTATUS.ColNo).Value().ToString()
                If chkVal = "01" Then
                    MyBase.ShowMessage("E850", New String() {Convert.ToString(Convert.ToInt32(arr(i)))})
                    Return False
                End If
            Next

        End With

        Return True

    End Function
#End Region

#End Region 'Method

End Class
