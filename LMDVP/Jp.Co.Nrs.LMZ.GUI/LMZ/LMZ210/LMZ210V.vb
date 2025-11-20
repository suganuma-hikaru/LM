' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ210V : 商品マスタ照会
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李

''' <summary>
''' LMZ210Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMZ210V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ210F
    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConV As LMZControlV

    '2017/09/25 修正 李↓
    '    ''' <summary>
    '    ''' 選択した言語を格納するフィールド
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '#If False Then '_LangFlgが初期化される前にアクセスしてされる問題の仮対応 20151109 INOUE
    '    Private _LangFlg As String
    '#Else
    '    Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    '#End If
    '2017/09/25 修正 李↑

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ210F, ByVal v As LMZControlV)

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

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '営業所
            '2015.11.02 tsunehira add Start
            '英語化対応
            .cmbNrsBrCd.ItemName = .LmTitleLabel4.TextValue
            '2015.11.02 tsunehira add End
            '.cmbNrsBrCd.ItemName = "営業所"
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            '荷主コード(大)

            '2017/09/25 修正 李↓
            .lblCustCdL.ItemName = lgm.Selector({"荷主コード(大)", "Custmer Code (L)", "하주코드(大)", "中国語"})
            '2017/09/25 修正 李↑

            .lblCustCdL.IsHissuCheck = True
            If MyBase.IsValidateCheck(.lblCustCdL) = False Then
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

            '届先名
            vCell.SetValidateCell(0, LMZ210G.sprDetailDef.DEST_NM.ColNo)
            '2015.11.02 tsunehira add Start
            '英語化対応
            vCell.ItemName = LMZ210G.sprDetailDef.DEST_NM.ColName
            '2015.11.02 tsunehira add End
            'vCell.ItemName = "届先名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '住所
            vCell.SetValidateCell(0, LMZ210G.sprDetailDef.AD_1.ColNo)
            '2015.11.02 tsunehira add Start
            '英語化対応
            vCell.ItemName = LMZ210G.sprDetailDef.AD_1.ColName
            '2015.11.02 tsunehira add End
            'vCell.ItemName = "住所"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 120
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'START YANAI 要望番号881
            '備考
            vCell.SetValidateCell(0, LMZ210G.sprDetailDef.REMARK.ColNo)
            '2015.11.02 tsunehira add Start
            '英語化対応
            vCell.ItemName = LMZ210G.sprDetailDef.REMARK.ColName
            '2015.11.02 tsunehira add End
            'vCell.ItemName = "備考"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            'END YANAI 要望番号881

            '届先コード
            vCell.SetValidateCell(0, LMZ210G.sprDetailDef.DEST_CD.ColNo)
            '2015.11.02 tsunehira add Start
            '英語化対応
            vCell.ItemName = LMZ210G.sprDetailDef.DEST_CD.ColName
            '2015.11.02 tsunehira add End
            'vCell.ItemName = "届先コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 15
            vCell.IsHankakuCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

#If True Then ' フィルメニッヒ セミEDI対応  20160930 added inoue
            ' 配送時注意事項
            vCell.SetValidateCell(0, LMZ210G.sprDetailDef.DELI_ATT.ColNo)
            vCell.ItemName = LMZ210G.sprDetailDef.DELI_ATT.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
#End If

        End With

        Return True


    End Function


#End Region 'Method

End Class
