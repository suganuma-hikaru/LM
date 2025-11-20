' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI130  : 日医工詰め合わせ画面
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMI130Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI130V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI130F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI130F, ByVal v As LMIControlV, ByVal g As LMI130G)

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
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI130C.EventShubetsu) As Boolean

        Dim valueDec As Decimal = 0

        '【単項目チェック】
        With Me._Frm

            If LMI130C.EventShubetsu.ADD.Equals(eventShubetsu) = True Then
                '出荷管理番号
                .txtOutkaNo.ItemName() = .lblOutkaNo.TextValue
                .txtOutkaNo.IsHissuCheck() = True
                .txtOutkaNo.IsForbiddenWordsCheck() = True
                .txtOutkaNo.IsByteCheck() = 15
                If MyBase.IsValidateCheck(.txtOutkaNo) = False Then
                    Return False
                End If
            End If

            If LMI130C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                '印刷種別
                .cmbPrint.ItemName() = .lblTitlePrint.TextValue
                .cmbPrint.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbPrint) = False Then
                    Return False
                End If
            End If

            If LMI130C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI130C.EventShubetsu.NIFUDAPRINT.Equals(eventShubetsu) = True OrElse _
                LMI130C.EventShubetsu.KONPOPRINT.Equals(eventShubetsu) = True Then
                '部数
                .numPrtCnt.ItemName() = "部数"
                .numPrtCnt.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.numPrtCnt) = False Then
                    Return False
                End If

                If String.IsNullOrEmpty(.numPrtCnt.TextValue) = False Then
                    If Me.IsHaniCheck(Convert.ToDecimal(.numPrtCnt.Value), 1, 99) = False Then
                        MyBase.ShowMessage("E014", New String() {"部数", "1", "99"})
                        Me._Vcon.SetErrorControl(.numPrtCnt)
                        Return False
                    End If
                End If
            End If


            'スプレッドのチェック
            Dim arr As ArrayList = Nothing
            arr = Me.GetCheckList(LMI130G.sprDetailsDef.DEF.ColNo, .sprDetails)
            Dim max As Integer = arr.Count - 1
            Dim sprmax As Integer = .sprDetails.ActiveSheet.Rows.Count - 1

            If LMI130C.EventShubetsu.ROWDEL.Equals(eventShubetsu) = True Then
                '未選択チェック
                If max < 0 Then
                    MyBase.ShowMessage("E009")
                    Return False
                End If
            End If

            If LMI130C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                LMI130C.EventShubetsu.NIFUDAPRINT.Equals(eventShubetsu) = True OrElse _
                LMI130C.EventShubetsu.KONPOPRINT.Equals(eventShubetsu) = True Then
                '0件チェック
                If sprmax < 0 Then
                    MyBase.ShowMessage("E483", New String() {"印刷"})
                    Return False
                End If
            End If

            If (LMI130C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso _
                ("01").Equals(.cmbPrint.SelectedValue) = True) OrElse _
                LMI130C.EventShubetsu.KONPOPRINT.Equals(eventShubetsu) = True Then
                '16件より多いチェック
                If 16 <= sprmax Then 'sprmaxは-1しているので、16で比較
                    MyBase.ShowMessage("E481", New String() {"詰め合わせ", "16"})
                    Return False
                End If
            End If

            Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetails)
            For i As Integer = 0 To sprmax

                If LMI130C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                    LMI130C.EventShubetsu.NIFUDAPRINT.Equals(eventShubetsu) = True OrElse _
                    LMI130C.EventShubetsu.KONPOPRINT.Equals(eventShubetsu) = True Then
                    '商品名
                    vCell.SetValidateCell(i, LMI130G.sprDetailsDef.GOODSNM1.ColNo)
                    vCell.ItemName = LMI130G.sprDetailsDef.GOODSNM1.ColName
                    vCell.IsHissuCheck = True
                    vCell.IsForbiddenWordsCheck = True
                    vCell.IsByteCheck = 60
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If
                End If

                If LMI130C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                    LMI130C.EventShubetsu.NIFUDAPRINT.Equals(eventShubetsu) = True OrElse _
                    LMI130C.EventShubetsu.KONPOPRINT.Equals(eventShubetsu) = True Then
                    '規格名
                    vCell.SetValidateCell(i, LMI130G.sprDetailsDef.GOODSNM2.ColNo)
                    vCell.ItemName = LMI130G.sprDetailsDef.GOODSNM2.ColName
                    vCell.IsForbiddenWordsCheck = True
                    vCell.IsByteCheck = 60
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If
                End If

                If LMI130C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                    LMI130C.EventShubetsu.NIFUDAPRINT.Equals(eventShubetsu) = True OrElse _
                    LMI130C.EventShubetsu.KONPOPRINT.Equals(eventShubetsu) = True Then
                    'ロット№
                    vCell.SetValidateCell(i, LMI130G.sprDetailsDef.LOTNO.ColNo)
                    vCell.ItemName = LMI130G.sprDetailsDef.LOTNO.ColName
                    vCell.IsForbiddenWordsCheck = True
                    vCell.IsByteCheck = 40
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If
                End If

                If LMI130C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                    LMI130C.EventShubetsu.NIFUDAPRINT.Equals(eventShubetsu) = True OrElse _
                    LMI130C.EventShubetsu.KONPOPRINT.Equals(eventShubetsu) = True Then
                    '詰め合わせ個数
                    vCell.SetValidateCell(i, LMI130G.sprDetailsDef.TSUMENB.ColNo)
                    vCell.ItemName = LMI130G.sprDetailsDef.TSUMENB.ColName
                    vCell.IsHissuCheck = True
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                    valueDec = Convert.ToDecimal(Me._Vcon.GetCellValue(.sprDetails.ActiveSheet.Cells(i, LMI130G.sprDetailsDef.TSUMENB.ColNo)))
                    If 10 < valueDec.ToString.Length Then
                        Me._Vcon.SetErrorControl(.sprDetails, i, LMI130G.sprDetailsDef.TSUMENB.ColNo)
                        Me._Vcon.SetErrMessage("E023", New String() {LMI130G.sprDetailsDef.TSUMENB.ColName, "10"})
                        Return False
                    End If

                End If

            Next

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
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI130C.EventShubetsu) As Boolean

        '【関連項目チェック】
        With Me._Frm

            'スプレッドのチェック
            Dim arr As ArrayList = Nothing
            arr = Me.GetCheckList(LMI130G.sprDetailsDef.DEF.ColNo, .sprDetails)
            Dim max As Integer = arr.Count - 1
            Dim sprmax As Integer = .sprDetails.ActiveSheet.Rows.Count - 1
            Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetails)

            Dim value As String = String.Empty

            For i As Integer = 0 To sprmax

                If (LMI130C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso _
                    ("01").Equals(.cmbPrint.SelectedValue) = True) OrElse _
                    LMI130C.EventShubetsu.KONPOPRINT.Equals(eventShubetsu) = True Then
                    If Convert.ToDecimal(Me._Vcon.GetCellValue(.sprDetails.ActiveSheet.Cells(i, LMI130G.sprDetailsDef.TSUMENB.ColNo))) > _
                        Convert.ToDecimal(Me._Vcon.GetCellValue(.sprDetails.ActiveSheet.Cells(i, LMI130G.sprDetailsDef.ALCTDNB.ColNo))) Then
                        '詰め合わせ個数 + 出荷個数
                        Me._Vcon.SetErrorControl(.sprDetails, i, LMI130G.sprDetailsDef.TSUMENB.ColNo)
                        Me._Vcon.SetErrMessage("E482", New String() {"詰め合わせ個数", "出荷個数"})
                        Return False
                    End If
                End If

                If LMI130C.EventShubetsu.PRINT.Equals(eventShubetsu) = True OrElse _
                    LMI130C.EventShubetsu.NIFUDAPRINT.Equals(eventShubetsu) = True OrElse _
                    LMI130C.EventShubetsu.KONPOPRINT.Equals(eventShubetsu) = True Then
                    value = Me._Vcon.GetCellValue(.sprDetails.ActiveSheet.Cells(0, LMI130G.sprDetailsDef.DESTCD.ColNo))
                    If value.Equals(Me._Vcon.GetCellValue(.sprDetails.ActiveSheet.Cells(i, LMI130G.sprDetailsDef.DESTCD.ColNo))) = False Then
                        '届先が異なるデータが選択されている場合エラー
                        '(選択された中で一番上の届先コードを保持し、それと比較して異なるデータがある場合にエラーと判定)
                        Me._Vcon.SetErrMessage("E227", New String() {"届先"})
                        Return False
                    End If
                End If

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI130C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI130C.EventShubetsu.ADD          '追加
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

            Case LMI130C.EventShubetsu.CLEAR        'クリア
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

            Case LMI130C.EventShubetsu.PRINT        '印刷
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

            Case LMI130C.EventShubetsu.CLOSE        '閉じる
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

            Case LMI130C.EventShubetsu.ROWDEL       '行削除
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
    Friend Function GetCheckList(ByVal defNo As Integer, ByVal sprDetail As Spread.LMSpread) As ArrayList

        Return Me._Vcon.SprSelectList2(defNo, sprDetail)

    End Function

    ''' <summary>
    ''' 範囲チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsHaniCheck(ByVal value As Decimal, ByVal minData As Decimal, ByVal maxData As Decimal) As Boolean

        If value < minData OrElse _
            maxData < value Then
            Return False
        End If

        Return True

    End Function

#End Region 'Method

End Class
