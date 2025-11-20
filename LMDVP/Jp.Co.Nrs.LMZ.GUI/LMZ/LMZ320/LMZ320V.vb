' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ320V : 支払横持ちタリフ照会
'  作  成  者       :  本明
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMZ320Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMZ320V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ320F

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMZ320G

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConV As LMZControlV

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConG As LMZControlG


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ320F, ByVal v As LMZControlV _
                   , ByVal LMZConG As LMZControlG, ByVal g As LMZ320G)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMZConV = v

        Me._LMZConG = LMZConG

        Me._G = g

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 単項目
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsInputChk(ByVal csFlg As String) As Boolean

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue()

        'Trim
        Call Me._LMZConV.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0, Me._Frm.sprDetail.ActiveSheet.Columns.Count - 1)


        '単項目チェック
        If Me.IsSingleCheck() = False Then
            Return False
        End If


        '荷主コード存在チェック(キャッシュ)
        If String.IsNullOrEmpty(Me._Frm.txtCustCdL.TextValue) = False _
            AndAlso csFlg.Equals(LMConst.FLG.ON) = False Then
            '画面ヘッダー部設定
            If Me.HeaderExist() = False Then
                Me._Frm.sprDetail.CrearSpread()
                Call Me._G.CustHeaderClear()
                Return False
            End If

        End If

        '単項目チェック
        If Me.IsSpreadInputChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue()

        With Me._Frm

            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()

        End With

    End Sub

    ''' <summary>
    ''' 画面ヘッダー部入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSingleCheck() As Boolean

        With Me._Frm

            '営業所
            .cmbNrsBrCd.ItemName = "営業所"
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            If String.IsNullOrEmpty(.txtCustCdL.TextValue) = False Then
                '荷主コード(大)
                .txtCustCdL.ItemName = "荷主コード(大)"
                .txtCustCdL.IsForbiddenWordsCheck = True
                .txtCustCdL.IsFullByteCheck = 5
                If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    Return False
                End If
            End If

            If String.IsNullOrEmpty(.txtCustCdM.TextValue) = False Then
                '荷主コード(中)
                .txtCustCdM.ItemName = "荷主コード(中)"
                .txtCustCdM.IsForbiddenWordsCheck = True
                .txtCustCdM.IsFullByteCheck = 2
                If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' スプレッドの項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadInputChk() As Boolean

        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            'タリフコード
            vCell.SetValidateCell(0, LMZ320G.sprDetailDef.YOKO_TARIFF_CD.ColNo)
            vCell.ItemName = "タリフコード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            vCell.IsHankakuCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If


            '備考
            vCell.SetValidateCell(0, LMZ320G.sprDetailDef.YOKO_REM.ColNo)
            vCell.ItemName = "備考"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True


    End Function


    ''' <summary>
    ''' 画面ヘッダー部：荷主マスタ存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function HeaderExist() As Boolean

        '荷主コードの取得
        Dim custCdL As String = Me._Frm.txtCustCdL.TextValue
        Dim custCdM As String = Me._Frm.txtCustCdM.TextValue

        If String.IsNullOrEmpty(custCdM) = True Then
            custCdM = "00"
            Me._Frm.txtCustCdM.TextValue = custCdM
        End If

        '画面ヘッダー部の設定(荷主キャッシュ)
        Dim headerRow As DataRow() = Me._LMZConG.SelectCustListDataRow(custCdL, custCdM)
        If 0 < headerRow.Length Then
            'ヘッダー部にデータをセット
            Call Me._G.CustHeaderDataSet(headerRow(0))
            Return True
        End If
        'エラー
        Call Me._LMZConV.SetMstErrMessage("荷主マスタ", "荷主コード")

        Return False

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim msg As String() = Nothing
        Dim clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl() = Nothing
        Dim focusCtl As Control = Me._Frm.ActiveControl

        With Me._Frm

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    Dim custNm As String = .lblTitleCust.Text
                    ctl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM}
                    msg = New String() {String.Concat(custNm, LMZControlC.CD, LMZControlC.L_NM) _
                                        , String.Concat(custNm, LMZControlC.CD, LMZControlC.M_NM)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblCustNmL, .lblCustNmM}


            End Select

            Return Me._LMZConV.IsFocusChk(ctl, msg, focusCtl, clearCtl)

        End With

    End Function

#End Region 'Method

End Class
