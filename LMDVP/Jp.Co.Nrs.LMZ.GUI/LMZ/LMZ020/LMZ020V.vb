' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ020V : 商品マスタ照会
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李

''' <summary>
''' LMZ020Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMZ020V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ020F
    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConV As LMZControlV

    '2017/09/25 修正 李↓
    '' ''' <summary>
    '' ''' 選択した言語を格納するフィールド
    '' ''' </summary>
    '' ''' <remarks></remarks>
    ''Private _LangFlg As String
    '2017/09/25 修正 李↑

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ020F, ByVal v As LMZControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

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
            '2015.11.02 tsunehira add Start
            '英語化対応
            .cmbNrsBrCd.ItemName = .LmTitleLabel2.TextValue
            '2015.11.02 tsunehira add End
            '.cmbNrsBrCd.ItemName = "営業所"
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If


            '荷主コード(大)
            '2015.11.02 tsunehira add Start
            '英語化対応
            .lblCustCdL.ItemName = LMZ020G.sprDetailDef.CUST_CD_L.ColName
            '2015.11.02 tsunehira add End
            '.lblCustCdL.ItemName = "荷主コード(大)"
            .lblCustCdL.IsHissuCheck = True
            If MyBase.IsValidateCheck(.lblCustCdL) = False Then
                Return False
            End If

            '荷主コード(中)
            '2015.11.02 tsunehira add Start
            '英語化対応
            .lblCustCdM.ItemName = LMZ020G.sprDetailDef.CUST_CD_M.ColName
            '2015.11.02 tsunehira add End
            '.lblCustCdM.ItemName = "荷主コード(中)"
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
            vCell.SetValidateCell(0, LMZ020G.sprDetailDef.GOODS_NM_1.ColNo)
            '2015.11.02 tsunehira add Start
            '英語化対応
            vCell.ItemName = LMZ020G.sprDetailDef.GOODS_NM_1.ColName
            '2015.11.02 tsunehira add End
            'vCell.ItemName = "正式名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品コード
            vCell.SetValidateCell(0, LMZ020G.sprDetailDef.GOODS_CD_CUST.ColNo)
            '2015.11.02 tsunehira add Start
            '英語化対応
            vCell.ItemName = LMZ020G.sprDetailDef.GOODS_CD_CUST.ColName
            '2015.11.02 tsunehira add End
            'vCell.ItemName = "商品コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            vCell.IsHankakuCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主カテゴリ1
            vCell.SetValidateCell(0, LMZ020G.sprDetailDef.SEARCH_KEY_1.ColNo)
            '2015.11.02 tsunehira add Start
            '英語化対応
            vCell.ItemName = LMZ020G.sprDetailDef.SEARCH_KEY_1.ColName
            '2015.11.02 tsunehira add End
            'vCell.ItemName = "荷主カテゴリ1"
            vCell.IsForbiddenWordsCheck = True
            'START YANAI 要望番号1065 荷主カテゴリのバイト変更
            'vCell.IsByteCheck = 20
            vCell.IsByteCheck = 25
            'END YANAI 要望番号1065 荷主カテゴリのバイト変更
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主カテゴリ2
            vCell.SetValidateCell(0, LMZ020G.sprDetailDef.SEARCH_KEY_2.ColNo)
            '2015.11.02 tsunehira add Start
            '英語化対応
            vCell.ItemName = LMZ020G.sprDetailDef.SEARCH_KEY_2.ColName
            '2015.11.02 tsunehira add End
            'vCell.ItemName = "荷主カテゴリ2"
            vCell.IsForbiddenWordsCheck = True
            'START YANAI 要望番号1065 荷主カテゴリのバイト変更
            'vCell.IsByteCheck = 20
            vCell.IsByteCheck = 25
            'END YANAI 要望番号1065 荷主カテゴリのバイト変更
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '消防コード
            vCell.SetValidateCell(0, LMZ020G.sprDetailDef.CUST_CD_L.ColNo)
            '2015.11.02 tsunehira add Start
            '英語化対応
            vCell.ItemName = LMZ020G.sprDetailDef.CUST_CD_L.ColName
            '2015.11.02 tsunehira add End
            'vCell.ItemName = "消防コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 3
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名(小)
            vCell.SetValidateCell(0, LMZ020G.sprDetailDef.CUST_NM_S.ColNo)
            '2015.11.02 tsunehira add Start
            '英語化対応
            vCell.ItemName = LMZ020G.sprDetailDef.CUST_NM_S.ColName
            '2015.11.02 tsunehira add End
            'vCell.ItemName = "荷主名(小)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名(極小)
            vCell.SetValidateCell(0, LMZ020G.sprDetailDef.CUST_NM_SS.ColNo)
            '2015.11.02 tsunehira add Start
            '英語化対応
            vCell.ItemName = LMZ020G.sprDetailDef.CUST_NM_SS.ColName
            '2015.11.02 tsunehira add End
            'vCell.ItemName = "荷主名(極小)"
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
