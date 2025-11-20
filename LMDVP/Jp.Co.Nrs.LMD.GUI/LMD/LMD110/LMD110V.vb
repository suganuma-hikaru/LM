' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD110F : 在庫振替検索
'  作  成  者       :  daikoku
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMD110Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMD110V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD110F

    ''' <summary>
    ''' チェックリストを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' エラー行を格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkListErr As ArrayList

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMDControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD110F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = New LMDControlV(handlerClass, DirectCast(frm, Form))

        Me._ChkList = New ArrayList()

        Me._ChkListErr = New ArrayList()

    End Sub

#End Region 'Constructor


#Region "権限チェック"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMD110C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMD110C.EventShubetsu.MASTER          'マスタ参照

                '50:外部の場合エラー
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

            Case LMD110C.EventShubetsu.KENSAKU          '検索

                '50:外部の場合エラー
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

            Case LMD110C.EventShubetsu.CLOSE           '閉じる
                'すべての権限許可
                kengenFlg = True


            Case LMD110C.EventShubetsu.PRINT         '印刷

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

        If kengenFlg = True Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If

    End Function
#End Region


#Region "単項目チェック"
    ''' <summary>
    ''' 検索時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKensakuSingleCheck() As Boolean

        'Trimチェック

        '検索

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue()

        'スプレッド項目のスペース除去
        'Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprSagyo, 0, Me._Frm.sprSagyo.ActiveSheet.Columns.Count - 1)


        With Me._Frm

            '【単項目チェック】

            '******************** ヘッダ項目の入力チェック ********************

            '営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

            '倉庫
            .cmbWare.ItemName() = "倉庫"
            .cmbWare.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbWare) = False Then
                Return False
            End If

            'From
            If .imdFurikaeDate_S.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"作業日From", "8"})
                Return False
            End If

            '作業日To
            If .imdFurikaeDate_E.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"作業日To", "8"})
                Return False
            End If

            '振替元荷主コード(大)
            .txtMotoCustCD_L.ItemName() = "荷主コード(大)"
            .txtMotoCustCD_L.IsForbiddenWordsCheck() = True
            .txtMotoCustCD_L.IsByteCheck() = 5
            If MyBase.IsValidateCheck(.txtMotoCustCD_L) = False Then
                Return False
            End If

            '振替元荷主コード(中)
            .txtMotoCustCD_M.ItemName() = "荷主コード(中)"
            .txtMotoCustCD_M.IsForbiddenWordsCheck() = True
            .txtMotoCustCD_M.IsByteCheck() = 2
            If MyBase.IsValidateCheck(.txtMotoCustCD_M) = False Then
                Return False
            End If

            '振替先荷主コード(大)
            .txtMotoCustCD_L.ItemName() = "荷主コード(大)"
            .txtMotoCustCD_L.IsForbiddenWordsCheck() = True
            .txtMotoCustCD_L.IsByteCheck() = 5
            If MyBase.IsValidateCheck(.txtMotoCustCD_L) = False Then
                Return False
            End If

            '振替先荷主コード(中)
            .txtMotoCustCD_M.ItemName() = "荷主コード(中)"
            .txtMotoCustCD_M.IsForbiddenWordsCheck() = True
            .txtMotoCustCD_M.IsByteCheck() = 2
            If MyBase.IsValidateCheck(.txtMotoCustCD_M) = False Then
                Return False
            End If

            '******************** スプレッド項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprFurrikae)

            'オーダー番号
            vCell.SetValidateCell(0, LMD110G.sprDetailDef.ORDER_NO.ColNo)
            vCell.ItemName() = "オーダー番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '振替管理番号
            vCell.SetValidateCell(0, LMD110G.sprDetailDef.FURI_NO.ColNo)
            vCell.ItemName() = "振替管理番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 8
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '振替元名称
            vCell.SetValidateCell(0, LMD110G.sprDetailDef.MOTO_CUST_NM.ColNo)
            vCell.ItemName() = "振替元名称"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '振替先名称
            vCell.SetValidateCell(0, LMD110G.sprDetailDef.SAKI_CUST_NM.ColNo)
            vCell.ItemName() = "振替先名称"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '振替先商品名称
            vCell.SetValidateCell(0, LMD110G.sprDetailDef.SAKI_GOODS_NM.ColNo)
            vCell.ItemName() = "振替先商品名称"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function
#End Region

#Region "関連チェック"
    ''' <summary>
    ''' 検索時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Public Function IsKensakuKanrenCheck() As Boolean

        With Me._Frm

            '振替日From + 振替日To

            '  振替日Fromより振替日Toが過去日の場合エラー()
            '  いずれも設定済 である場合のみチェック

            If .imdFurikaeDate_S.TextValue.Equals(String.Empty) = False _
                And .imdFurikaeDate_E.TextValue.Equals(String.Empty) = False Then

                If .imdFurikaeDate_E.TextValue < .imdFurikaeDate_S.TextValue Then

                    '振替日Fromより振替日Toが過去日の場合エラー
                    Me.ShowMessage("E039", New String() {"振替日To", "振替日From"})
                    .imdFurikaeDate_S.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdFurikaeDate_E.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdFurikaeDate_S.Focus()
                    Return False

                End If

            End If

        End With

        Return True

    End Function

#End Region


#Region "単項目チェック"
    ''' <summary>
    ''' 削除チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsRowDelSingleCheck() As Boolean

        With Me._Frm

            '【単項目チェック】

            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            'スプレッド項目の入力チェック
            Dim max As Integer = Me._ChkList.Count - 1
            Dim chkVal As String = String.Empty
            Dim errVal As Integer = 0


        End With

        Return True

    End Function
#End Region

#Region "関連チェック"

#Region "マスタ参照時チェック"

    ''' <summary>
    ''' マスタ参照チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsPopSingleCheck(ByVal objNM As String) As Boolean

        With Me._Frm

            '営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

            Select Case objNM

                Case "txtMotoCustCD_L", "txtMotoCustCD_M"                    '荷主マスタ参照

                    '荷主コード(大)
                    .txtMotoCustCD_L.ItemName() = "荷主コード(大)"
                    .txtMotoCustCD_L.IsForbiddenWordsCheck() = True
                    .txtMotoCustCD_L.IsByteCheck() = 5
                    If MyBase.IsValidateCheck(.txtMotoCustCD_L) = False Then
                        Return False
                    End If

                    '荷主コード(中)
                    .txtMotoCustCD_M.ItemName() = "荷主コード(中)"
                    .txtMotoCustCD_M.IsForbiddenWordsCheck() = True
                    .txtMotoCustCD_M.IsByteCheck() = 2
                    If MyBase.IsValidateCheck(.txtMotoCustCD_M) = False Then
                        Return False
                    End If

                Case "txtSakiCustCD_L", "txtSakiCustCD_M"                    '荷主マスタ参照

                    '荷主コード(大)
                    .txtSakiCustCD_L.ItemName() = "荷主コード(大)"
                    .txtSakiCustCD_L.IsForbiddenWordsCheck() = True
                    .txtSakiCustCD_L.IsByteCheck() = 5
                    If MyBase.IsValidateCheck(.txtSakiCustCD_L) = False Then
                        Return False
                    End If

                    '荷主コード(中)
                    .txtSakiCustCD_M.ItemName() = "荷主コード(中)"
                    .txtSakiCustCD_M.IsForbiddenWordsCheck() = True
                    .txtSakiCustCD_M.IsByteCheck() = 2
                    If MyBase.IsValidateCheck(.txtSakiCustCD_M) = False Then
                        Return False
                    End If

            End Select

            Return True

        End With

        Return True

    End Function

#End Region

#Region "印刷時チェック"

    ''' <summary>
    ''' 印刷イベントの入力チェック
    ''' </summary>
    ''' <param name="printShubetsu">印刷種別</param>
    ''' <param name="arr">リスト</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsPrintInputCheck(ByVal printShubetsu As LMD110C.PrintShubetsu, ByVal arr As ArrayList, Optional EventShubetsu As LMD110C.EventShubetsu = LMD110C.EventShubetsu.PRINT) As Boolean

        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        With Me._Frm

            '印刷種別
            .cmbPrint.ItemName() = lgm.Selector({"印刷種別", "Printing type", "인쇄종별", "中国語"})

            .cmbPrint.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbPrint) = False Then
                Return False
            End If

            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region

#Region "内部メソッド"

#Region "マスタ存在チェック"
    ''' <summary>
    ''' ユーザーマスタの存在チェック
    ''' </summary>
    ''' <param name="text">チェック対象文字列</param>
    ''' <returns>ユーザー名</returns>
    ''' <remarks></remarks>
    Private Function IsExistUserNm(ByVal text As String) As String

        ''存在チェック
        Dim userNm As String = String.Empty

        IsExistUserNm = userNm

    End Function
#End Region

#Region "選択チェック"
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

        If Me._Vcon.IsSelectOneChk(Me._ChkList.Count()) = False Then
            MyBase.ShowMessage("E008")
            Return False
        End If

        Return True

    End Function
#End Region

#Region "単一選択チェック"
    ''' <summary>
    ''' 単一選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSelectOneDataChk() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Me._ChkList = Me.getCheckList()

        '選択チェック
        If Me._Vcon.IsSelectOneChk(Me._ChkList.Count()) = False Then
            MyBase.ShowMessage("E008")
            Return False
        End If

        Return True

    End Function
#End Region

#Region "選択行取得"
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMD110C.SprColumnIndex.DEF

        Return Me._Vcon.SprSelectList(defNo, Me._Frm.sprFurrikae)

    End Function

#End Region

#Region "数値Nullcheck"

    Private Function NumNullCheck(ByVal val As String) As Boolean


        If String.IsNullOrEmpty(val) = True Then
            MyBase.ShowMessage("E008")
            Return False

        End If

        Return True

    End Function
#End Region


    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue()

        With Me._Frm

            .txtMotoCustCD_L.TextValue = Me._Frm.txtMotoCustCD_L.TextValue.Trim()
            .txtMotoCustCD_M.TextValue = Me._Frm.txtMotoCustCD_M.TextValue.Trim()

        End With

    End Sub

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



#End Region

#End Region 'Method

#Region "ユーティリティ"

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="ctl">エラーコントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal ctl As Control)

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor()

        If TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            DirectCast(ctl, Win.InputMan.LMComboKubun).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            DirectCast(ctl, Win.InputMan.LMImNumber).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImDate = True Then

            DirectCast(ctl, Win.InputMan.LMImDate).BackColorDef = errorColor


        End If

        ctl.Focus()
        ctl.Select()

    End Sub

    ''' <summary>
    ''' 四捨五入
    ''' </summary>
    ''' <param name="decValue"></param>
    ''' <param name="iDigits"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ToRound(ByVal decValue As Decimal, ByVal iDigits As Integer) As Decimal

        Dim dCoef As Double = System.Math.Pow(10, iDigits)

        If decValue > 0 Then
            Return Convert.ToDecimal(Math.Floor((decValue * dCoef) + 0.5) / dCoef)
        Else
            Return Convert.ToDecimal(Math.Ceiling((decValue * dCoef) - 0.5) / dCoef)
        End If
    End Function

#End Region

End Class
