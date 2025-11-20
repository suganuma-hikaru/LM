' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ021V : 商品マスタ(在庫)照会
'  作  成  者       :  Annen
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMZ021Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMZ021V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ021F
    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConV As LMZControlV

    ''' <summary>
    ''' 選択した言語を格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LangFlg As String

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ021F, ByVal v As LMZControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        '選んだ言語の取得
        _LangFlg = MessageManager.MessageLanguage

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
            '英語化対応
            .cmbNrsBrCd.ItemName = .LmTitleLabel2.TextValue
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If


            '荷主コード(大)
            '英語化対応
            .lblCustCdL.ItemName = LMZ021G.sprDetailDef.CUST_CD_L.ColName
            .lblCustCdL.IsHissuCheck = True
            If MyBase.IsValidateCheck(.lblCustCdL) = False Then
                Return False
            End If

            '荷主コード(中)
            '英語化対応
            .lblCustCdM.ItemName = LMZ021G.sprDetailDef.CUST_CD_M.ColName
            .lblCustCdM.IsHissuCheck = True
            If MyBase.IsValidateCheck(.lblCustCdM) = False Then
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

            '正式名
            vCell.SetValidateCell(0, LMZ021G.sprDetailDef.GOODS_NM_1.ColNo)
            '英語化対応
            vCell.ItemName = LMZ021G.sprDetailDef.GOODS_NM_1.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品コード
            vCell.SetValidateCell(0, LMZ021G.sprDetailDef.GOODS_CD_CUST.ColNo)
            '英語化対応
            vCell.ItemName = LMZ021G.sprDetailDef.GOODS_CD_CUST.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            vCell.IsHankakuCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主カテゴリ1
            vCell.SetValidateCell(0, LMZ021G.sprDetailDef.SEARCH_KEY_1.ColNo)
            '英語化対応
            vCell.ItemName = LMZ021G.sprDetailDef.SEARCH_KEY_1.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 25
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主カテゴリ2
            vCell.SetValidateCell(0, LMZ021G.sprDetailDef.SEARCH_KEY_2.ColNo)
            '英語化対応
            vCell.ItemName = LMZ021G.sprDetailDef.SEARCH_KEY_2.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 25
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '消防コード
            vCell.SetValidateCell(0, LMZ021G.sprDetailDef.CUST_CD_L.ColNo)
            '英語化対応
            vCell.ItemName = LMZ021G.sprDetailDef.CUST_CD_L.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 3
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名(小)
            vCell.SetValidateCell(0, LMZ021G.sprDetailDef.CUST_NM_S.ColNo)
            '英語化対応
            vCell.ItemName = LMZ021G.sprDetailDef.CUST_NM_S.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名(極小)
            vCell.SetValidateCell(0, LMZ021G.sprDetailDef.CUST_NM_SS.ColNo)
            '英語化対応
            vCell.ItemName = LMZ021G.sprDetailDef.CUST_NM_SS.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True


    End Function


#End Region 'Method

End Class
