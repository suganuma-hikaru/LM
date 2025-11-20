' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ260V : 荷主マスタ照会（大・中）
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMZ260Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMZ260V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ260F

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ260F, ByVal v As LMZControlV)

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
            '2016.01.06 UMANO 英語化対応START
            '.cmbNrsBrCd.ItemName = "営業所"
            .cmbNrsBrCd.ItemName = .LmTitleLabel2.Text()
            '2016.01.06 UMANO 英語化対応END
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

            '荷主名(大)
            vCell.SetValidateCell(0, LMZ260G.sprDetailDef.CUST_NM_L.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主名(大)"
            vCell.ItemName = LMZ260G.sprDetailDef.CUST_NM_L.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名(中)
            vCell.SetValidateCell(0, LMZ260G.sprDetailDef.CUST_NM_M.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主名(中)"
            vCell.ItemName = LMZ260G.sprDetailDef.CUST_NM_M.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名(小)
            vCell.SetValidateCell(0, LMZ260G.sprDetailDef.CUST_NM_S.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主名(小)"
            vCell.ItemName = LMZ260G.sprDetailDef.CUST_NM_S.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名(極小)
            vCell.SetValidateCell(0, LMZ260G.sprDetailDef.CUST_NM_SS.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主名(極小)"
            vCell.ItemName = LMZ260G.sprDetailDef.CUST_NM_SS.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主コード(大)
            vCell.SetValidateCell(0, LMZ260G.sprDetailDef.CUST_CD_L.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主コード(大)"
            vCell.ItemName = LMZ260G.sprDetailDef.CUST_CD_L.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主コード(中)
            vCell.SetValidateCell(0, LMZ260G.sprDetailDef.CUST_CD_M.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主コード(中)"
            vCell.ItemName = LMZ260G.sprDetailDef.CUST_CD_M.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主コード(小)
            vCell.SetValidateCell(0, LMZ260G.sprDetailDef.CUST_CD_S.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主コード(小)"
            vCell.ItemName = LMZ260G.sprDetailDef.CUST_CD_S.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主コード(極小)
            vCell.SetValidateCell(0, LMZ260G.sprDetailDef.CUST_CD_SS.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主コード(極小)"
            vCell.ItemName = LMZ260G.sprDetailDef.CUST_CD_SS.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True


    End Function

#End Region 'Method

End Class
