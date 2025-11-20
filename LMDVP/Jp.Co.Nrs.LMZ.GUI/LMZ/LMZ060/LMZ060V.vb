' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ060 : 郵便番号マスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMZ060Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMZ060V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ060F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConV As LMZControlV


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ060F, ByVal v As LMZControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMZConV = v

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
        Call Me._LMZConV.TrimSpaceSprTextvalue(Me._Frm.sprZip, 0, Me._Frm.sprZip.ActiveSheet.Columns.Count - 1)

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
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprZip)


            '郵便番号
            vCell.SetValidateCell(0, LMZ060G.sprDetailDef.ZIP_NO.ColNo)
            vCell.ItemName = "郵便番号"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 7
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '市区町村名
            vCell.SetValidateCell(0, LMZ060G.sprDetailDef.CITY_N.ColNo)
            vCell.ItemName = "市区町村名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '町域名
            vCell.SetValidateCell(0, LMZ060G.sprDetailDef.TOWN_N.ColNo)
            vCell.ItemName = "町域名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'JISコード
            vCell.SetValidateCell(0, LMZ060G.sprDetailDef.JIS_CD.ColNo)
            vCell.ItemName = "JISコード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 7
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True


    End Function

#End Region 'Method

End Class
