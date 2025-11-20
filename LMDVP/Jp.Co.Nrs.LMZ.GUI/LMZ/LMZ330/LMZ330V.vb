' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ330V : UNマスタ照会
'  作  成  者       :  asatsuma
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMZ330Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMZ330V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ330F

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMZ330G

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ330F, ByVal v As LMZControlV _
                   , ByVal LMZConG As LMZControlG, ByVal g As LMZ330G)

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
    Friend Function IsInputChk() As Boolean

        'Trim
        Call Me._LMZConV.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0, Me._Frm.sprDetail.ActiveSheet.Columns.Count - 1)

        '単項目チェック
        If Me.IsSpreadInputChk() = False Then
            Return False
        End If

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

            'UN
            vCell.SetValidateCell(0, LMZ330G.sprDetailDef.UN_NO.ColNo)
            vCell.ItemName = "UN"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 4
            vCell.IsHankakuCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'UN
            vCell.SetValidateCell(0, LMZ330G.sprDetailDef.PG_KBN.ColNo)
            vCell.ItemName = "PG"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            vCell.IsHankakuCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'クラス(正)
            vCell.SetValidateCell(0, LMZ330G.sprDetailDef.IMDG_CLASS.ColNo)
            vCell.ItemName = "クラス(正)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsMojisuCheck = 3
            vCell.IsHankakuCheck = False
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'クラス(副)
            vCell.SetValidateCell(0, LMZ330G.sprDetailDef.IMDG_CLASS1.ColNo)
            vCell.ItemName = "クラス(副)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            vCell.IsHankakuCheck = False
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'クラス(副)
            vCell.SetValidateCell(0, LMZ330G.sprDetailDef.IMDG_CLASS2.ColNo)
            vCell.ItemName = "クラス(副)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 3
            vCell.IsHankakuCheck = False
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '海洋汚染物質
            vCell.SetValidateCell(0, LMZ330G.sprDetailDef.MP_FLG.ColNo)
            vCell.ItemName = "海洋汚染物質"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 1
            vCell.IsHankakuCheck = False
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True


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

            End Select

            Return Me._LMZConV.IsFocusChk(ctl, msg, focusCtl, clearCtl)

        End With

    End Function

#End Region 'Method

End Class
