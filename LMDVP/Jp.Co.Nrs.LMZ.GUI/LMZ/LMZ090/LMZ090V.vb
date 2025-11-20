' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ090V : エリアマスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMZ090Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMZ090V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ090F
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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ090F, ByVal v As LMZControlV)

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
        Call Me._LMZConV.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0, Me._Frm.sprDetail.ActiveSheet.Columns.Count - 1)


        '単項目チェック
        If Me.IsSingleCheck() = False Then
            Return False
        End If


        '単項目チェック
        If Me.IsSpreadInputChk() = False Then
            Return False
        End If

        Return True

    End Function

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

            'エリアコード
            vCell.SetValidateCell(0, LMZ090G.sprDetailDef.AREA_CD.ColNo)
            vCell.ItemName = "エリアコード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 7
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'エリア名
            vCell.SetValidateCell(0, LMZ090G.sprDetailDef.AREA_NM.ColNo)
            vCell.ItemName = "エリア名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If


            '備考
            vCell.SetValidateCell(0, LMZ090G.sprDetailDef.AREA_INFO.ColNo)
            vCell.ItemName = "備考"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True


    End Function


#End Region 'Method

End Class
